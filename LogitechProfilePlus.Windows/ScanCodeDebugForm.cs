using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogitechProfilePlus.Windows
{
    public partial class ScanCodeDebugForm : Form
    {
        public ScanCodeDebugForm()
        {
            InitializeComponent();
        }

        #region ScanCode_Debug
        //private void btnScanCodeUp_Click(object sender, EventArgs e)
        //{
        //    _currentDebugScanCode++;
        //    txtScanCodeDebug.Text = _currentDebugScanCode.ToString();
        //    TestScanCode();
        //}

        //private void btnScanCodeDown_Click(object sender, EventArgs e)
        //{
        //    _currentDebugScanCode--;
        //    txtScanCodeDebug.Text = _currentDebugScanCode.ToString();
        //    TestScanCode();
        //}

        //private void btnScanCodeUpdate_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        _currentDebugScanCode = Convert.ToInt32(txtScanCodeDebug.Text);
        //        txtScanCodeDebug.Text = _currentDebugScanCode.ToString();
        //        TestScanCode();
        //    }
        //    catch (Exception)
        //    {

        //        lstScanCodeHistory.Items.Add("Conversion error");
        //    }
        //}

        //private void TestScanCode()
        //{
        //    if (!LedCSharp.LogitechGSDK.LogiLedSetLightingForKeyWithScanCode(_currentDebugScanCode, 0, 100, 0))
        //    {
        //        lstScanCodeHistory.Items.Add(_currentDebugScanCode.ToString() + " error");
        //    }
        //    else
        //    {
        //        lstScanCodeHistory.Items.Add(_currentDebugScanCode.ToString());
        //    }
        //}
        #endregion
    }
}
