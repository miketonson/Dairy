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



namespace DiaryManager
{
    public partial class Form1 : Form
    {
        //构造函数在这里！
        public Form1()
        {
            InitializeComponent();
            sv.CookieContainer = new System.Net.CookieContainer();
            sv.logoutCompleted += sv_logoutCompleted;
            sv.getNewestDiaryCompleted += sv_getNewestDiaryCompleted;
            setLoggedOut();
            
        }

        void sv_getNewestDiaryCompleted(object sender, getNewestDiaryCompletedEventArgs e)
        {
            workspace.Text = e.Result.content;
            //throw new NotImplementedException();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox_size_Click(object sender, EventArgs e)
        {

        }
        


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public bool isLoggedIn = false;
        public Service1 sv = new Service1();
        //窗体加载时的初始化
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (FontFamily font in FontFamily.Families)      //窗体打开时加载系统安装字体名称并输入到ComboBox控件中去  
            {
                this.toolStripComboBox_style.Items.Add(font.Name);           //加载  
            }
            this.toolStripComboBox_style.SelectedItem = "宋体";              //默认显示的字体  

            this.toolStripComboBox_size.SelectedIndex = 10;               //默认的字号为10  
        }

        private void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        //加粗
        private void toolStripButton_bold_Click(object sender, EventArgs e)
        {
            Font oldFont, newFont;
            oldFont = this.workspace.SelectionFont;
            if (oldFont.Bold)
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Bold);
            else
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Bold);
            this.workspace.SelectionFont = newFont;
            this.workspace.Focus();
        }

        //斜体
        private void toolStripButton_Italic_Click(object sender, EventArgs e)
        {
            Font oldFont, newFont;
            oldFont = this.workspace.SelectionFont;
            if (oldFont.Italic)
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Italic);
            else
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Italic);
            this.workspace.SelectionFont = newFont;
            this.workspace.Focus();
        }

        //下划线
        private void toolStripButton_underline_Click(object sender, EventArgs e)
        {
            Font oldFont, newFont;
            oldFont = this.workspace.SelectionFont;
            if (oldFont.Underline)
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Underline);
            else
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Underline);
            this.workspace.SelectionFont = newFont;
            this.workspace.Focus();
        }

        //更改字号
        private void toolStripComboBox_size_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sizeString = ((ToolStripComboBox)sender).SelectedItem.ToString();
            float curSize = float.Parse(sizeString);
            Font oldFont = this.workspace.SelectionFont;
            this.workspace.SelectionFont = new Font(oldFont.FontFamily, curSize, oldFont.Style);
            this.workspace.Focus();
        }

        //更改字体
        private void toolStripComboBox_style_SelectedIndexChanged(object sender, EventArgs e)
        {
            string styleString = ((ToolStripComboBox)sender).SelectedItem.ToString();

            Font oldFont = this.workspace.SelectionFont;
            //try
            //{
                this.workspace.SelectionFont = new Font(styleString, oldFont.Size, oldFont.Style);
            //}
           // catch (Exception )
            //{
            //    MessageBox.Show("请使用另一种字体");
           // }
            this.workspace.Focus();
        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox_style_Click(object sender, EventArgs e)
        {

        }


        private void toolStripTextBox1_Click_1(object sender, EventArgs e)
        {

        }
        public string username;

        private void setLoggedIn()
        {
            this.未登录ToolStripMenuItem.Text = "已登录：" + username;
            this.登录ToolStripMenuItem.Text = "注销(&T)";
            this.登录ToolStripMenuItem.Visible = true;

            foreach (ToolStripItem t in this.未登录ToolStripMenuItem.DropDownItems)
                t.Visible = true;
        }

        private void setLoggedOut()
        {
            this.isLoggedIn = false;
            this.登录ToolStripMenuItem.Visible = false;
            this.未登录ToolStripMenuItem.Text = "（未登录）";

            foreach (ToolStripItem t in this.未登录ToolStripMenuItem.DropDownItems)
                t.Visible = false;
        }

        private void 未登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.isLoggedIn)
            {
                this.登录ToolStripMenuItem.Visible = false;
                frmLogin fl = new frmLogin() { Owner = this };
                fl.ShowDialog();
                if (this.isLoggedIn)
                {
                    setLoggedIn();
                }
            }
            
        }

        private void 登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.isLoggedIn)
            {
                sv.logoutAsync();
                
            }
        }

        void sv_logoutCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("已注销。");
            setLoggedOut();
        }

        private void 获取最新日记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sv.getNewestDiaryAsync();
            
        }

        private void 上传当前日记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        //保存日记到本地
        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "rtf files (*.rtf)|*.rtf|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    // Code to write the stream goes here.
                    workspace.SaveFile(myStream,RichTextBoxStreamType.RichText);
                    myStream.Close();
                }
            }
        }

        //保存框
        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
        }

        //新建日记，日期默认为当天的日期
        private void 新建日记ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 读取日记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "rtf files (*.rtf)|*.rtf|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            workspace.LoadFile(myStream, RichTextBoxStreamType.RichText);
                            // Insert code to read the stream here.
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 newForm = new Form2();
            newForm.Show();
        }
    }
}
