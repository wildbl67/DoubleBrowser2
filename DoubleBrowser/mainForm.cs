﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Collections;

namespace DoubleBrowser
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();


            //this.treeViewLeft.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.treeViewLeft_MouseClick);
        }

       


       

        private void mainForm_Load(object sender, EventArgs e)

        {
            
            MyTrees treeClass = new MyTrees();

            TreeNode lib = new TreeNode("My Computer", 0, 0);
            TreeNode libright = new TreeNode("My Computer", 0, 0);

            lib.ImageKey = "desktop";
            libright.ImageKey = "desktop";
            treeViewLeft.Nodes.Add(lib);
            treeViewRight.Nodes.Add(libright);
            try
            {

                string fpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                //imageListIcons.Images.Add(Icon.ExtractAssociatedIcon(fpath));
                string pPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string picPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                string cPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                //  string access = Directory.GetAccessControl(pPath).ToString();
                //  Console.WriteLine("Path check " + pPath);
                //  Console.WriteLine("Path Access" + access);

                DirectoryInfo md = new DirectoryInfo(pPath);

                lib.Nodes.Add(fpath);
                lib.Nodes.Add(md.Name);
                lib.Nodes.Add(picPath);

                libright.Nodes.Add(fpath);
                libright.Nodes.Add(pPath);
                libright.Nodes.Add(picPath);


                treeClass.PopulateTreeView(fpath, "desktop", treeViewLeft);
                treeClass.PopulateTreeView(pPath, "documents", treeViewLeft);
                treeClass.PopulateTreeView(picPath, "documents", treeViewLeft);
                treeClass.PopulateTreeView(cPath, "music", treeViewLeft);
                treeClass.PopulateTreeView(fpath, "desktop", treeViewRight);
                treeClass.PopulateTreeView(pPath, "documents", treeViewRight);
                treeClass.PopulateTreeView(cPath, "music", treeViewRight);


               treeClass.TreeViewDrives(treeViewLeft);

                treeClass.TreeViewDrives(treeViewRight);
            }
           
            catch(System.NullReferenceException ex) { return; }
            catch (Exception ex) {
                string denideMessage = ex.Message;
                if (denideMessage.Contains("denide access")) { return; } else { MessageBox.Show(ex.Message.ToString(), "Load exception" + ex.InnerException.ToString(), MessageBoxButtons.OK); }

            }

           
           
            //PopulateTreeView(pPath);
            //lib.Nodes.Add(PopulateTreeView(cPath,"music"));





        }
        private void treeViewLeft_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            {
                TreeNode newSelected = e.Node;
                listViewLeft.Items.Clear();
                DirectoryInfo nodeDirInfo = (DirectoryInfo)newSelected.Tag;
                ListViewItem.ListViewSubItem[] subItems;
                ListViewItem item = null;
                Icon iconForFile = SystemIcons.WinLogo;

                foreach (DirectoryInfo dir in nodeDirInfo.GetDirectories())
                {

                    item = new ListViewItem(dir.Name, 0);

                    item.ImageKey = dir.Extension;


                    //iconForFile = Icon.ExtractAssociatedIcon(dir.FullName);
                    subItems = new ListViewItem.ListViewSubItem[] {

                        new ListViewItem.ListViewSubItem(item, "Directory"),
                       new ListViewItem.ListViewSubItem(item, dir.LastAccessTime.ToShortDateString())
                    };
                    item.SubItems.AddRange(subItems);
                    listViewLeft.Items.Add(item);
                }

                foreach (FileInfo file in nodeDirInfo.GetFiles())
                {
                    iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
                    imageListIcons.Images.Add(file.Extension, iconForFile);
                    item = new ListViewItem(file.Name, 1);
                    item.ImageKey = file.Extension;
                    subItems = new ListViewItem.ListViewSubItem[]
                        { new ListViewItem.ListViewSubItem(item, "File"),
             new ListViewItem.ListViewSubItem(item, file.LastAccessTime.ToShortDateString())};

                    item.SubItems.AddRange(subItems);
                    listViewLeft.Items.Add(item);
                }

                listViewLeft.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }
        /// <summary>
        /// 
        /// </summary>

    }
}
