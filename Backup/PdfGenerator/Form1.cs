using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using System.IO;


namespace PdfGenerator
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            ListFieldNames();
            FillForm();
        }


        /// <summary>
        /// List all of the form fields into a textbox.  The
        /// form fields identified can be used to map each of the
        /// fields in a PDF.
        /// </summary>
        private void ListFieldNames()
        {
            string pdfTemplate = @"c:\Temp\PDF\fw4.pdf";

            // title the form
            this.Text += " - " + pdfTemplate;

            // create a new PDF reader based on the PDF template document
            PdfReader pdfReader = new PdfReader(pdfTemplate);

            // create and populate a string builder with each of the 
            // field names available in the subject PDF
            StringBuilder sb = new StringBuilder();
            foreach (DictionaryEntry de in pdfReader.AcroFields.Fields)
            {
                sb.Append(de.Key.ToString() + Environment.NewLine);
            }

            // Write the string builder's content to the form's textbox
            textBox1.Text = sb.ToString();
            textBox1.SelectionStart = 0;
        }


        private void FillForm()
        {
            string pdfTemplate = @"c:\Temp\PDF\fw4.pdf";
            string newFile = @"c:\Temp\PDF\completed_fw4.pdf";

            PdfReader pdfReader = new PdfReader(pdfTemplate);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(
                        newFile, FileMode.Create));
            
            AcroFields pdfFormFields = pdfStamper.AcroFields;

            // set form pdfFormFields

            // The first worksheet and W-4 form
            pdfFormFields.SetField("f1_01(0)", "1");   
            pdfFormFields.SetField("f1_02(0)", "1");
            pdfFormFields.SetField("f1_03(0)", "1");
            pdfFormFields.SetField("f1_04(0)", "8");
            pdfFormFields.SetField("f1_05(0)", "0");
            pdfFormFields.SetField("f1_06(0)", "1");
            pdfFormFields.SetField("f1_07(0)", "16");
            pdfFormFields.SetField("f1_08(0)", "28");
            pdfFormFields.SetField("f1_09(0)", "Franklin A.");
            pdfFormFields.SetField("f1_10(0)", "Benefield");
            pdfFormFields.SetField("f1_11(0)", "532");
            pdfFormFields.SetField("f1_12(0)", "12");
            pdfFormFields.SetField("f1_13(0)", "1234");

            // The form's checkboxes
            pdfFormFields.SetField("c1_01(0)", "0");
            pdfFormFields.SetField("c1_02(0)", "Yes");
            pdfFormFields.SetField("c1_03(0)", "0");
            pdfFormFields.SetField("c1_04(0)", "Yes");

            // The rest of the form pdfFormFields
            pdfFormFields.SetField("f1_14(0)", "100 North Cujo Street");
            pdfFormFields.SetField("f1_15(0)", "Nome, AK  67201");
            pdfFormFields.SetField("f1_16(0)", "9");
            pdfFormFields.SetField("f1_17(0)", "10");
            pdfFormFields.SetField("f1_18(0)", "11");
            pdfFormFields.SetField("f1_19(0)", "Walmart, Nome, AK");
            pdfFormFields.SetField("f1_20(0)", "WAL666");
            pdfFormFields.SetField("f1_21(0)", "AB");
            pdfFormFields.SetField("f1_22(0)", "4321");

            // Second Worksheets pdfFormFields
            // In order to map the fields, I just pass them a sequential
            // number to mark them; once I know which field is which, I 
            // can pass the appropriate value
            pdfFormFields.SetField("f2_01(0)", "1");
            pdfFormFields.SetField("f2_02(0)", "2");
            pdfFormFields.SetField("f2_03(0)", "3");
            pdfFormFields.SetField("f2_04(0)", "4");
            pdfFormFields.SetField("f2_05(0)", "5");
            pdfFormFields.SetField("f2_06(0)", "6");
            pdfFormFields.SetField("f2_07(0)", "7");
            pdfFormFields.SetField("f2_08(0)", "8");
            pdfFormFields.SetField("f2_09(0)", "9");
            pdfFormFields.SetField("f2_10(0)", "10");
            pdfFormFields.SetField("f2_11(0)", "11");
            pdfFormFields.SetField("f2_12(0)", "12");
            pdfFormFields.SetField("f2_13(0)", "13");
            pdfFormFields.SetField("f2_14(0)", "14");
            pdfFormFields.SetField("f2_15(0)", "15");
            pdfFormFields.SetField("f2_16(0)", "16");
            pdfFormFields.SetField("f2_17(0)", "17");
            pdfFormFields.SetField("f2_18(0)", "18");
            pdfFormFields.SetField("f2_19(0)", "19");

            // report by reading values from completed PDF
            string sTmp = "W-4 Completed for " + pdfFormFields.GetField("f1_09(0)") + " " +
                pdfFormFields.GetField("f1_10(0)");
            MessageBox.Show(sTmp, "Finished");

            // flatten the form to remove editting options, set it to false
            // to leave the form open to subsequent manual edits
            pdfStamper.FormFlattening = false;

            // close the pdf
            pdfStamper.Close();
        }

    }
}