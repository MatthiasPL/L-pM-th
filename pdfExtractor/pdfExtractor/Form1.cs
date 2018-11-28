﻿using iTextSharp.text.pdf;
//using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pdfExtractor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static string filePath;
        static string Author = "", Title = "", Creator = "", Subject = "", Keywords = "", CreatedDate = "";
        static PdfReader myreader;
        WorkPiece workpiece = new WorkPiece(myreader, filePath, Author, Title, Creator, Subject, Keywords, CreatedDate); 

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void ShowPDFbutton_Click(object sender, EventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();
            
            dlg.Filter = "PDF Files (*.PDF)|*.PDF|All Files (*.*)|*.*";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                filePath = dlg.FileName.ToString();
                workpiece.FilePath = filePath;
                string pdftext = string.Empty;
                try
                {
                    PdfReader reader = new PdfReader(filePath);
                    //workpiece.MyReader = reader;
                    FileInfo myfilepath = new FileInfo(filePath);
                    workpiece.GetInfoFromPDF(reader, myfilepath);
                    reader.Close();

                    AuthortextBox.Text = workpiece.Author;
                    TitletextBox.Text = workpiece.Title;
                    CreatortextBox.Text = workpiece.Creator;
                    SubjecttextBox.Text = workpiece.Subject;
                    KeywordstextBox.Text = workpiece.Keywords;
                    CreationDatetextBox.Text = workpiece.CreatedDate;

                    IDictionary<String, String> metadic = reader.Info;
                    var dic = from m in metadic
                              select m;
                    foreach (var d in dic)
                    {
                        MetadatalistBox.Items.Add(d.Key + ": " + d.Value);         
                    }
                    reader.Close();                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void SaveDatabutton_Click(object sender, EventArgs e)
        {
            workpiece.Author = AuthortextBox.Text;
            workpiece.Title = TitletextBox.Text;
            workpiece.Creator = CreatortextBox.Text;
            workpiece.Subject = SubjecttextBox.Text;
            workpiece.Keywords = KeywordstextBox.Text;
            workpiece.CreatedDate = CreationDatetextBox.Text;

            workpiece.SetInfo(workpiece.FilePath, "Author", AuthortextBox.Text);
            workpiece.SetInfo(workpiece.FilePath, "Title", TitletextBox.Text);
            workpiece.SetInfo(workpiece.FilePath, "Creator", CreatortextBox.Text);
            workpiece.SetInfo(workpiece.FilePath, "Subject", SubjecttextBox.Text);
            workpiece.SetInfo(workpiece.FilePath, "Keywords", KeywordstextBox.Text);
            workpiece.SetInfo(workpiece.FilePath, "CreatedDate", CreationDatetextBox.Text);
        }
    }
}