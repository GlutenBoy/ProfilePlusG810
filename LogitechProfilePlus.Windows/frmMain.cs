using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LogitechProfilePlus.Windows
{
    public partial class frmMain : Form
    {
        const string PROFILES_JSON_FILENAME = "./profiles.json";
        Dictionary<String, Control> _keyboardKeysDict;
        Profiles _profiles = new Profiles();
        ProfileData _currentProfile;
        bool _exitApplication = false;
        int _currentDebugScanCode = 1000;

        /// <summary>
        /// Constructor
        /// </summary>
        public frmMain()
        {
            InitializeComponent();

            if (!LedCSharp.LogitechGSDK.LogiLedInit())
            {
                MessageBox.Show("Error in LogiLedInit");
                Application.Exit();
            }
            else
            {
                LedCSharp.LogitechGSDK.LogiLedSaveCurrentLighting();
            }

            SetKeysTag();

            // Set handlers for all keys and create a dictionnary of keys
            // Example: btnF1.Click += KeyboardClick;
            _keyboardKeysDict = new Dictionary<String, Control>();
            foreach (Control c in pnlKeyboard.Controls)
            {
                string tagText;
                c.Click += KeyboardClick;
                c.BackColor = Color.DimGray;

                try
                {
                    tagText = c.Tag.ToString();
                    _keyboardKeysDict.Add(tagText, c);
                }
                catch (Exception)
                {
                    // skip this since key as no tag
                }
            }

            LoadProfiles(PROFILES_JSON_FILENAME);
        }

    #region Form Events
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_exitApplication) { 
                e.Cancel = true;
                Visible = false;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //SaveProfiles();
            
            Application.Exit();
        }
    #endregion

        private void SetKeysTag()
        {
            lblLogo.Tag = LedCSharp.KeyboardNames.G_LOGO.ToString();
            btnESC.Tag = LedCSharp.KeyboardNames.ESC.ToString();

            btnF1.Tag = LedCSharp.KeyboardNames.F1.ToString();
            btnF2.Tag = LedCSharp.KeyboardNames.F2.ToString();
            btnF3.Tag = LedCSharp.KeyboardNames.F3.ToString();
            btnF4.Tag = LedCSharp.KeyboardNames.F4.ToString();

            btnF5.Tag = LedCSharp.KeyboardNames.F5.ToString();
            btnF6.Tag = LedCSharp.KeyboardNames.F6.ToString();
            btnF7.Tag = LedCSharp.KeyboardNames.F7.ToString();
            btnF8.Tag = LedCSharp.KeyboardNames.F8.ToString();

            btnF9.Tag = LedCSharp.KeyboardNames.F9.ToString();
            btnF10.Tag = LedCSharp.KeyboardNames.F10.ToString();
            btnF11.Tag = LedCSharp.KeyboardNames.F11.ToString();
            btnF12.Tag = LedCSharp.KeyboardNames.F12.ToString();

            btnTilde.Tag = LedCSharp.KeyboardNames.TILDE.ToString();
            btn1.Tag = LedCSharp.KeyboardNames.ONE.ToString();
            btn2.Tag = LedCSharp.KeyboardNames.TWO.ToString();
            btn3.Tag = LedCSharp.KeyboardNames.THREE.ToString();
            btn4.Tag = LedCSharp.KeyboardNames.FOUR.ToString();
            btn5.Tag = LedCSharp.KeyboardNames.FIVE.ToString();
            btn6.Tag = LedCSharp.KeyboardNames.SIX.ToString();
            btn7.Tag = LedCSharp.KeyboardNames.SEVEN.ToString();
            btn8.Tag = LedCSharp.KeyboardNames.EIGHT.ToString();
            btn9.Tag = LedCSharp.KeyboardNames.NINE.ToString();
            btn0.Tag = LedCSharp.KeyboardNames.ZERO.ToString();
            btnMinus.Tag = LedCSharp.KeyboardNames.MINUS.ToString();
            btnEqual.Tag = LedCSharp.KeyboardNames.EQUALS.ToString();
            btnBackspace.Tag = LedCSharp.KeyboardNames.BACKSPACE.ToString();

            btnTab.Tag = LedCSharp.KeyboardNames.TAB.ToString();
            btnQ.Tag = LedCSharp.KeyboardNames.Q.ToString();
            btnW.Tag = LedCSharp.KeyboardNames.W.ToString();
            btnE.Tag = LedCSharp.KeyboardNames.E.ToString();
            btnR.Tag = LedCSharp.KeyboardNames.R.ToString();
            btnT.Tag = LedCSharp.KeyboardNames.T.ToString();
            btnY.Tag = LedCSharp.KeyboardNames.Y.ToString();
            btnU.Tag = LedCSharp.KeyboardNames.U.ToString();
            btnI.Tag = LedCSharp.KeyboardNames.I.ToString();
            btnO.Tag = LedCSharp.KeyboardNames.O.ToString();
            btnP.Tag = LedCSharp.KeyboardNames.P.ToString();
            btnBracketLeft.Tag = LedCSharp.KeyboardNames.OPEN_BRACKET.ToString();
            btnBracketRight.Tag = LedCSharp.KeyboardNames.CLOSE_BRACKET.ToString();
            btnBackSlash.Tag = LedCSharp.KeyboardNames.BACKSLASH.ToString();

            btnCapsLock.Tag = LedCSharp.KeyboardNames.CAPS_LOCK.ToString();
            btnA.Tag = LedCSharp.KeyboardNames.A.ToString();
            btnS.Tag = LedCSharp.KeyboardNames.S.ToString();
            btnD.Tag = LedCSharp.KeyboardNames.D.ToString();
            btnF.Tag = LedCSharp.KeyboardNames.F.ToString();
            btnG.Tag = LedCSharp.KeyboardNames.G.ToString();
            btnH.Tag = LedCSharp.KeyboardNames.H.ToString();
            btnJ.Tag = LedCSharp.KeyboardNames.J.ToString();
            btnK.Tag = LedCSharp.KeyboardNames.K.ToString();
            btnL.Tag = LedCSharp.KeyboardNames.L.ToString();
            btnSemiColon.Tag = LedCSharp.KeyboardNames.SEMICOLON.ToString();
            btnQuote.Tag = LedCSharp.KeyboardNames.APOSTROPHE.ToString();
            btnEnter.Tag = LedCSharp.KeyboardNames.ENTER.ToString();

            btnShiftLeft.Tag = LedCSharp.KeyboardNames.LEFT_SHIFT.ToString();
            btnZ.Tag = LedCSharp.KeyboardNames.Z.ToString();
            btnX.Tag = LedCSharp.KeyboardNames.X.ToString();
            btnC.Tag = LedCSharp.KeyboardNames.C.ToString();
            btnV.Tag = LedCSharp.KeyboardNames.V.ToString();
            btnB.Tag = LedCSharp.KeyboardNames.B.ToString();
            btnN.Tag = LedCSharp.KeyboardNames.N.ToString();
            btnM.Tag = LedCSharp.KeyboardNames.M.ToString();
            btnComma.Tag = LedCSharp.KeyboardNames.COMMA.ToString();
            btnDot.Tag = LedCSharp.KeyboardNames.PERIOD.ToString();
            btnSlash.Tag = LedCSharp.KeyboardNames.FORWARD_SLASH.ToString();
            btnShiftRight.Tag = LedCSharp.KeyboardNames.RIGHT_SHIFT.ToString();

            btnCtrlLeft.Tag = LedCSharp.KeyboardNames.LEFT_CONTROL.ToString();
            btnWinLeft.Tag = LedCSharp.KeyboardNames.LEFT_WINDOWS.ToString();
            btnAltLeft.Tag = LedCSharp.KeyboardNames.LEFT_ALT.ToString();
            btnSpace.Tag = LedCSharp.KeyboardNames.SPACE.ToString();
            btnAltRight.Tag = LedCSharp.KeyboardNames.RIGHT_ALT.ToString();
            btnWinRight.Tag = LedCSharp.KeyboardNames.RIGHT_WINDOWS.ToString();
            btnContextualMenu.Tag = LedCSharp.KeyboardNames.APPLICATION_SELECT.ToString();
            btnCtrlRight.Tag = LedCSharp.KeyboardNames.RIGHT_CONTROL.ToString();

            btnPrintScreen.Tag = LedCSharp.KeyboardNames.PRINT_SCREEN.ToString();
            btnScrollLock.Tag = LedCSharp.KeyboardNames.SCROLL_LOCK.ToString();
            btnPause.Tag = LedCSharp.KeyboardNames.PAUSE_BREAK.ToString();

            btnInsert.Tag = LedCSharp.KeyboardNames.INSERT.ToString();
            btnHome.Tag = LedCSharp.KeyboardNames.HOME.ToString();
            btnPageUp.Tag = LedCSharp.KeyboardNames.PAGE_UP.ToString();
            btnDelete.Tag = LedCSharp.KeyboardNames.KEYBOARD_DELETE.ToString();
            btnEnd.Tag = LedCSharp.KeyboardNames.END.ToString();
            btnPageDown.Tag = LedCSharp.KeyboardNames.PAGE_DOWN.ToString();

            btnArrowUp.Tag = LedCSharp.KeyboardNames.ARROW_UP.ToString();
            btnArrowLeft.Tag = LedCSharp.KeyboardNames.ARROW_LEFT.ToString();
            btnArrowDown.Tag = LedCSharp.KeyboardNames.ARROW_DOWN.ToString();
            btnArrowRight.Tag = LedCSharp.KeyboardNames.ARROW_RIGHT.ToString();

            btnNumLock.Tag = LedCSharp.KeyboardNames.NUM_LOCK.ToString();
            btnNumpadSlash.Tag = LedCSharp.KeyboardNames.NUM_SLASH.ToString();
            btnNumpadStar.Tag = LedCSharp.KeyboardNames.NUM_ASTERISK.ToString();
            btnNumpadMinus.Tag = LedCSharp.KeyboardNames.NUM_MINUS.ToString();

            btnNumpad1.Tag = LedCSharp.KeyboardNames.NUM_ONE.ToString();
            btnNumpad2.Tag = LedCSharp.KeyboardNames.NUM_TWO.ToString();
            btnNumpad3.Tag = LedCSharp.KeyboardNames.NUM_THREE.ToString();
            btnNumpad4.Tag = LedCSharp.KeyboardNames.NUM_FOUR.ToString();
            btnNumpad5.Tag = LedCSharp.KeyboardNames.NUM_FIVE.ToString();
            btnNumpad6.Tag = LedCSharp.KeyboardNames.NUM_SIX.ToString();
            btnNumpad7.Tag = LedCSharp.KeyboardNames.NUM_SEVEN.ToString();
            btnNumPad8.Tag = LedCSharp.KeyboardNames.NUM_EIGHT.ToString();
            btnNumpad9.Tag = LedCSharp.KeyboardNames.NUM_NINE.ToString();
            btnNumpad0.Tag = LedCSharp.KeyboardNames.NUM_ZERO.ToString();

            btnNumpadPLus.Tag = LedCSharp.KeyboardNames.NUM_PLUS.ToString();
            btnNumpadEnter.Tag = LedCSharp.KeyboardNames.NUM_ENTER.ToString();
            btnNumpadDot.Tag = LedCSharp.KeyboardNames.NUM_PERIOD.ToString();
        }

        private void KeyboardClick(object sender, EventArgs e)
        {
            Control keyboardPreviewKey;

            keyboardPreviewKey = ((Control)sender);

            if (_currentProfile is null)
            {
                MessageBox.Show("Select a profile first.");
                return;
            }

            if ((keyboardPreviewKey.Tag != null) && ((string)keyboardPreviewKey.Tag != ""))
            {
                SetKeyProfile(keyboardPreviewKey, picCurrentColor.BackColor, _currentProfile);
                SetLEDPreview(keyboardPreviewKey, picCurrentColor.BackColor);
                SetLEDHardware((string)keyboardPreviewKey.Tag, picCurrentColor.BackColor);
            }
        }


        /// <summary>
        /// Set a key color in the designated profile.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="color"></param>
        /// <param name="profile"></param>
        private void SetKeyProfile(Control key, Color color, ProfileData profile)
        {
            KeyData foundKey = null;
            foreach (KeyData k in _currentProfile.KeyList)
            {
                if (k.Tag == key.Tag.ToString())
                {
                    foundKey = k;
                    break;
                }
            }

            if (foundKey is null)
            {
                // if we don't find the key in the profile we create one
                KeyData newKey = new KeyData()
                {
                    Name = key.Tag.ToString(),
                    Tag = key.Tag.ToString(),
                    R = picCurrentColor.BackColor.R,
                    G = picCurrentColor.BackColor.G,
                    B = picCurrentColor.BackColor.B
                };
                profile.KeyList.Add(newKey);
            }
            else
            {
                // if the key alredy exists in the profile we update it.
                foundKey.R = picCurrentColor.BackColor.R;
                foundKey.G = picCurrentColor.BackColor.G;
                foundKey.B = picCurrentColor.BackColor.B;
            }
        }
        private void SetAllKeysLED(Color color)
        {
            if (_currentProfile is null)
            {
                MessageBox.Show("Select a profile first.");
                return;
            }

            foreach (Control c in _keyboardKeysDict.Values)
            {
                SetKeyProfile(c, color, _currentProfile);
                SetLEDPreview(c, color);
                SetLEDHardware((string)c.Tag, color);
            }
        }

        private int ColorPercentage(int colorValue) {
            return colorValue * 100 / 255;
        }

        private void LoadProfiles(String filename)
        {
            if (!File.Exists(filename))
            {
                _profiles = new Profiles();
            }
            else
            {
                String profilesJSON;
                StreamReader jsonFile = new StreamReader(filename);
                profilesJSON = jsonFile.ReadToEnd();
                jsonFile.Close();
                _profiles = Newtonsoft.Json.JsonConvert.DeserializeObject<Profiles>(profilesJSON);

                ToolStripMenuItem newToolStripMenuItem;

                foreach (ProfileData p in _profiles.ProfileList)
                {
                    newToolStripMenuItem = new ToolStripMenuItem(p.Name, null, ToolStripMenuItemProfile_click)
                    {
                        Tag = p
                    };

                    NotificationMenu.Items.Add(newToolStripMenuItem);
                }

                lvwColors.Items.Clear();
                foreach (Color c in _profiles.ColorList)
                {
                    ListViewItem newlvwItem = new ListViewItem("")
                    {
                        BackColor = c
                    };
                    lvwColors.Items.Add(newlvwItem);
                }
            }

            lstProfiles.Items.Clear();
            lstProfiles.DataSource = _profiles.ProfileList;
            lstProfiles.DisplayMember = "Name";
        }

        private void SaveProfiles()
        {
            LedCSharp.LogitechGSDK.LogiLedShutdown();

            // Saving all profiles
            String profilesJSON;
            profilesJSON = Newtonsoft.Json.JsonConvert.SerializeObject(_profiles);

            StreamWriter jsonFile = new StreamWriter(PROFILES_JSON_FILENAME);
            jsonFile.Write(profilesJSON);
            jsonFile.Close();
        }

        private void ToolStripMenuItemProfile_click(object sender, EventArgs e)
        {
            ToolStripMenuItem profileToolStripMenuItem;

            profileToolStripMenuItem = (ToolStripMenuItem)sender;
            _currentProfile = (ProfileData)profileToolStripMenuItem.Tag;
            lblCurentProfile.Text = _currentProfile.Name;
            SetLEDsfromProfile(_currentProfile);
        }

        private void PicCurrentColor_Click(object sender, EventArgs e)
        {
            CP.ColorPicker cp = new CP.ColorPicker()
            {
                SelectedColor = picCurrentColor.BackColor
            };
            cp.ShowDialog();
            picCurrentColor.BackColor = cp.SelectedColor;
        }

        // ----- PROFILES UI ------
        #region Profiles

        private void BtnNewProfile_Click(object sender, EventArgs e)
        {

            ProfileNameDialog pnd = new ProfileNameDialog()
            {
                ProfileName = ""
            };
            pnd.ShowDialog(this);

            if (pnd.DialogResult == DialogResult.Cancel) return;

            if (pnd.ProfileName != "")
            {
                // Verify in the profile name already exists
                foreach (ProfileData p in _profiles.ProfileList)
                {
                    if (p.Name == pnd.ProfileName)
                    {
                        MessageBox.Show("Name already exists.");
                        return;
                    }
                }

                if (pnd.DialogResult == DialogResult.OK && pnd.ProductName != "")
                {
                    // Create the new profile
                    ProfileData newProfile = new ProfileData(pnd.ProfileName);
                    _profiles.ProfileList.Add(newProfile);

                    ToolStripMenuItem newToolStripMenuItem = new ToolStripMenuItem(newProfile.Name, null, ToolStripMenuItemProfile_click)
                    {
                        Tag = newProfile
                    };

                    NotificationMenu.Items.Add(newToolStripMenuItem);
                }

            }


        }

        private void btnDeleteProfile_Click(object sender, EventArgs e)
        {
            ProfileData profileToDelete;
            profileToDelete = (ProfileData)lstProfiles.SelectedItem;

            lstProfiles.SelectedItem = null;
            lblCurentProfile.Text = "none";

            _profiles.ProfileList.Remove(profileToDelete);
        }

        private void btnRenameProfile_Click(object sender, EventArgs e)
        {
            ProfileData profileToRename;
            profileToRename = (ProfileData)lstProfiles.SelectedItem;

            if (profileToRename != null)
            {
                ProfileNameDialog pnd = new ProfileNameDialog()
                {
                    ProfileName = profileToRename.Name
                };
                pnd.ShowDialog(this);

                if (pnd.DialogResult == DialogResult.OK && pnd.ProductName != "")
                {
                    profileToRename.Name = pnd.ProfileName;

                    foreach (ToolStripMenuItem tsi in NotificationMenu.Items.OfType<ToolStripMenuItem>())
                    {
                        if (tsi.Tag == profileToRename)
                        {
                            tsi.Text = profileToRename.Name;
                        }
                    }
                }
            }
        }

        private void lstProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentProfile = (ProfileData)lstProfiles.SelectedItem;

            if (_currentProfile != null)
            {
                lblCurentProfile.Text = _currentProfile.Name;
                SetLEDsfromProfile(_currentProfile);
            }
        }

        #endregion

        private void SetLEDsfromProfile(ProfileData profile)
        {
            Control keyboardPreviewKey;

            foreach (Control c in _keyboardKeysDict.Values)
            {
                c.BackColor = Color.DimGray;
            }

            foreach (KeyData key in profile.KeyList)
            {
                Color LEDColor;
                keyboardPreviewKey = _keyboardKeysDict[key.Tag];
                LEDColor = Color.FromArgb(key.R, key.G, key.B);

                SetLEDPreview(keyboardPreviewKey, LEDColor);
                SetLEDHardware(key.Tag, LEDColor);
            }
        }

        private void SetLEDPreview(Control control, Color LEDColor)
        {
            string tag = control.Tag.ToString();

            switch (tag)
            {
                case "numlocklight": // "mute" // This doesn't work yet we don't have the proper code for it
                    control.BackColor = LEDColor;
                    break;
                default:
                    control.ForeColor = LEDColor;
                    break;
            }

            control.BackColor = Color.Black;
        }

        private void SetLEDHardware(string tagText, Color LEDColor)
        {
            LedCSharp.KeyboardNames keyboardName;
            int redPercentage, greenPercentage, bluePercentage;

            redPercentage = ColorPercentage(LEDColor.R);
            greenPercentage = ColorPercentage(LEDColor.G);
            bluePercentage = ColorPercentage(LEDColor.B);

            keyboardName = (LedCSharp.KeyboardNames)System.Enum.Parse(typeof(LedCSharp.KeyboardNames), tagText);

            switch (tagText)
            {
                case "logo":
                    if (!LedCSharp.LogitechGSDK.LogiLedSetLightingForKeyWithKeyName(LedCSharp.KeyboardNames.G_LOGO, redPercentage, greenPercentage, bluePercentage))
                    {
                        MessageBox.Show("Error setting color!");
                    }
                    break;
                case "numlocklight": // This doesn't work yet we don't have the proper code for it
                    if (!LedCSharp.LogitechGSDK.LogiLedSetLightingForKeyWithKeyName(LedCSharp.KeyboardNames.G_2, redPercentage, greenPercentage, bluePercentage))
                    {
                        // ?
                    }
                    break;
                case "mute": // This doesn't work yet we don't have the proper code for it
                    if (!LedCSharp.LogitechGSDK.LogiLedSetLightingForKeyWithScanCode(0x01, redPercentage, greenPercentage, bluePercentage))
                    {
                        // ?
                    }
                    break;
                default:
                    if (!LedCSharp.LogitechGSDK.LogiLedSetLightingForKeyWithKeyName(keyboardName, redPercentage, greenPercentage, bluePercentage))
                    {
                        // ?
                    }
                    break;
            }
        }

    #region Color Events
        private void btnAddColor_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem lvwi in lvwColors.Items)
            {
                if (picCurrentColor.BackColor == lvwi.BackColor)
                {
                    MessageBox.Show("Color alredy exists");
                    return;
                }
            }

            ListViewItem newlvwItem = new ListViewItem("")
            {
                BackColor = picCurrentColor.BackColor
            };
            lvwColors.Items.Add(newlvwItem);
            _profiles.ColorList.Add(picCurrentColor.BackColor);
        }

        private void btnDeleteColor_Click(object sender, EventArgs e)
        {
            if (lvwColors.SelectedItems.Count == 1)
            {
                _profiles.ColorList.Remove(lvwColors.SelectedItems[0].BackColor);
                lvwColors.Items.Remove(lvwColors.SelectedItems[0]);
            }
        }

        private void lvwColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwColors.SelectedItems.Count == 1)
            {
                picCurrentColor.BackColor = lvwColors.SelectedItems[0].BackColor;
            }
        }

        private void btnSetAllKeyColor_Click(object sender, EventArgs e)
        {
            SetAllKeysLED(picCurrentColor.BackColor);
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            LedCSharp.LogitechGSDK.LogiLedRestoreLighting();
        }
    #endregion

        private void resetToLogitechDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LedCSharp.LogitechGSDK.LogiLedRestoreLighting();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveProfiles();
            _exitApplication = true;
            NotifyIcon.Visible = false;
            Application.Exit();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Visible = true;
        }
    }
}
