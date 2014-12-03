﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace LibCommonControl
{
    public partial class SelectRuleUserControl : UserControl
    {
        int ruleId;
        string ruleDescription;

        public SelectRuleUserControl()
        {
            InitializeComponent();
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            SelectRuleDlg dlg = new SelectRuleDlg();
            dlg.ShowDialog();
            if (System.Windows.Forms.DialogResult.OK == dlg.DialogResult)
            {
                this.ruleId = dlg.ruleId;
                this.ruleDescription = dlg.ruleDescription;

                int index = -1;
                bool alreadyExist = false;
                foreach (RuleSimple rule in this.cbbRule.Items)
                {
                    index++;
                    if (rule.Id == dlg.ruleId)
                    {
                        alreadyExist = true;
                        break;
                    }
                }

                // remove the tunnel that already exists.
                if (alreadyExist)
                    cbbRule.Items.RemoveAt(index);

                RuleSimple ts = new RuleSimple(dlg.ruleId, dlg.ruleDescription);
                // Set the new selected tunnel.
                index = cbbRule.Items.Add(ts);
                cbbRule.SelectedIndex = index;

                // Write the recent used tunnel to XML
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;

                XmlWriter writer = XmlWriter.Create("RecentRules.xml", settings);
                writer.WriteStartDocument();
                writer.WriteComment("This file is generated by the program.");

                writer.WriteStartElement("Rules");

                for (int i = 1; i < this.cbbRule.Items.Count; i++)
                {
                    RuleSimple rule = this.cbbRule.Items[i] as RuleSimple;
                    writer.WriteStartElement("Rule");

                    writer.WriteElementString("ID", rule.Id.ToString());   // <-- These are new
                    writer.WriteElementString("Description", rule.Description);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();

                writer.Flush();
                writer.Close();
            }
        }

        private void SelectRuleUserControl_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(@"RecentRules.xml"))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@"RecentRules.xml");

                XmlNodeList nodes = doc.DocumentElement.SelectNodes("/Rules/Rule");

                //List<TunnelSimple> tunnels = new List<TunnelSimple>();

                RuleSimple firstTS = new RuleSimple(-1, "已选择的规则");
                this.cbbRule.Items.Add(firstTS);

                foreach (XmlNode node in nodes)
                {
                    string id = node.SelectSingleNode("ID").InnerText;
                    string des = node.SelectSingleNode("Description").InnerText;
                    RuleSimple tunnel = new RuleSimple(int.Parse(id), des);
                    cbbRule.Items.Add(tunnel);
                }

                cbbRule.SelectedIndex = 0;
            }
        }
    }
}
