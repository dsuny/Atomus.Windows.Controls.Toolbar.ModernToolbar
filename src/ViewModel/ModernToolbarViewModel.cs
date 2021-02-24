using Atomus.Control;
using Atomus.Diagnostics;
using Atomus.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Atomus.Windows.Controls.Toolbar.ViewModel
{
    public class ModernToolbarViewModel : Atomus.MVVM.ViewModel
    {
        #region Declare
        private string backColor;
        private List<ComboxItem> comboxMenuItem;
        private ComboxItem comboxMenuItemSelected;
        private ImageSource image1;
        private string image2;
        private ImageSource image3;


        private List<ActionButton> actionButtons;
        private ActionButton actionButtonSelected;

        public class ComboxItem : Atomus.MVVM.ViewModel
        {
            #region Declare
            private decimal menuID;
            private decimal parentMenuID;
            private string name;
            private string description;
            private ImageSource imageSource;
            #endregion

            #region Property
            public decimal MenuID
            {
                get
                {
                    return this.menuID;
                }
                set
                {
                    if (this.menuID != value)
                    {
                        this.menuID = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            public decimal ParentMenuID
            {
                get
                {
                    return this.parentMenuID;
                }
                set
                {
                    if (this.parentMenuID != value)
                    {
                        this.parentMenuID = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            public string Name
            {
                get
                {
                    return this.name;
                }
                set
                {
                    if (this.name != value)
                    {
                        this.name = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            public string Description
            {
                get
                {
                    return this.description;
                }
                set
                {
                    if (this.description != value)
                    {
                        this.description = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }

            public ImageSource Image
            {
                get
                {
                    return this.imageSource;
                }
                set
                {
                    if (this.imageSource != value)
                    {
                        this.imageSource = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            #endregion
        }

        public class ActionButton : Atomus.MVVM.ViewModel
        {
            #region Declare
            private string name;
            private string text;
            private Visibility visibility;
            private bool isWindowsControl;
            private ImageSource imageSource;
            private ImageSource imageSourceOver;
            private ImageSource imageSourcePressed;
            #endregion

            #region Property
            public string Name
            {
                get
                {
                    return this.name;
                }
                set
                {
                    if (this.name != value)
                    {
                        this.name = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            public string Text
            {
                get
                {
                    return this.text;
                }
                set
                {
                    if (this.text != value)
                    {
                        this.text = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            public Visibility Visibility
            {
                get
                {
                    return this.visibility;
                }
                set
                {
                    if (this.visibility != value)
                    {
                        this.visibility = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            public bool IsWindowsControl
            {
                get
                {
                    return this.isWindowsControl;
                }
                set
                {
                    if (this.isWindowsControl != value)
                    {
                        this.isWindowsControl = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            public ImageSource Image
            {
                get
                {
                    return this.imageSource;
                }
                set
                {
                    if (this.imageSource != value)
                    {
                        this.imageSource = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            public ImageSource ImageOver
            {
                get
                {
                    return this.imageSourceOver;
                }
                set
                {
                    if (this.imageSourceOver != value)
                    {
                        this.imageSourceOver = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            public ImageSource ImagePressed
            {
                get
                {
                    return this.imageSourcePressed;
                }
                set
                {
                    if (this.imageSourcePressed != value)
                    {
                        this.imageSourcePressed = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            #endregion
        }

        #endregion

        #region Property
        public ICore Core { get; set; }

        public string BackColor
        {
            get
            {
                return this.backColor;
            }
            set
            {
                if (this.backColor != value)
                {
                    this.backColor = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public List<ComboxItem> ComboxMenuItem
        {
            get
            {
                return this.comboxMenuItem;
            }
            set
            {
                if (this.comboxMenuItem != value)
                {
                    this.comboxMenuItem = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        public ComboxItem ComboxMenuItemSelected
        {
            get
            {
                return this.comboxMenuItemSelected;
            }
            set
            {
                if (this.comboxMenuItemSelected != value)
                {
                    this.comboxMenuItemSelected = value;
                    this.NotifyPropertyChanged();

                    if (this.comboxMenuItemSelected != null)
                        (this.Core as IAction).ControlAction(this.Core, "Menu.TopMenuSelected", new object[] { this.comboxMenuItemSelected.MenuID, -1M });
                    else
                        (this.Core as IAction).ControlAction(this.Core, "Menu.TopMenuSelected", new object[] { -1M, -1M });
                }
            }
        }
        public ImageSource Image1
        {
            get
            {
                return this.image1;
            }
            set
            {
                if (this.image1 != value)
                {
                    this.image1 = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        public string Image2
        {
            get
            {
                return this.image2;
            }
            set
            {
                if (this.image2 != value)
                {
                    this.image2 = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        public ImageSource Image3
        {
            get
            {
                return this.image3;
            }
            set
            {
                if (this.image3 != value)
                {
                    this.image3 = value;
                    this.NotifyPropertyChanged();
                }
            }
        }


        public List<ActionButton> ActionButtons
        {
            get
            {
                return this.actionButtons;
            }
            set
            {
                if (this.actionButtons != value)
                {
                    this.actionButtons = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        public ActionButton ActionButtonSelected
        {
            get
            {
                return this.actionButtonSelected;
            }
            set
            {
                if (this.actionButtonSelected != value)
                {
                    this.actionButtonSelected = value;
                    this.NotifyPropertyChanged();

                    if (this.actionButtonSelected != null && this.actionButtonSelected.Name != "Menu")
                        (this.Core as IAction).ControlAction(this.Core, this.actionButtonSelected.Name, null);

                    //var aa = from sel in this.actionButtons
                    //         where sel.Name == "Menu"
                    //         select sel;

                    //if (aa != null && aa.Count() == 1)
                    //    this.ActionButtonSelected = aa.ToList()[0];

                    //if (this.comboxMenuItemSelected != null)
                    //    (this.Core as IAction).ControlAction(this.Core, "Menu.TopMenuSelected", new object[] { this.comboxMenuItemSelected.MenuID, -1M });
                    //else
                    //    (this.Core as IAction).ControlAction(this.Core, "Menu.TopMenuSelected", new object[] { -1M, -1M });
                }
            }
        }

        //public ICommand TestCommand { get; set; }
        #endregion

        #region INIT
        public ModernToolbarViewModel()
        {
            //this.TestCommand = new MVVM.DelegateCommand(() => { this.RibbonButtonProcess(null); }
            //                                                , () => { return true; });
        }
        public ModernToolbarViewModel(ICore core) : this()
        {
            this.Core = core;

            try
            {
                this.backColor = this.Core.GetAttribute("BackColor");

                this.SetActionButtons();
            }
            catch (Exception ex)
            {
                DiagnosticsTool.MyTrace(ex);
            }
        }

        private bool IsSetActionButtons = false;
        public async void SetActionButtons()
        {
            string tmp;
            string tmpImgae;
            string tmpImgaeOver;
            string tmpImgaePressed;
            string[] buttons;
            string[] iswindowscontrol;
            List<ActionButton> actionButton;

            try
            {
                this.IsSetActionButtons = true;
                actionButton = new List<ActionButton>();

                tmp = this.Core.GetAttribute("Buttons");

                if (tmp.IsNullOrEmpty())
                    return;

                buttons = tmp.Split(',');

                tmp = this.Core.GetAttribute("ButtonsWIndowsControl");

                if (tmp.IsNullOrEmpty())
                    return;

                iswindowscontrol = tmp.Split(',');

                foreach (string bnt in buttons)
                {
                    tmpImgae = this.Core.GetAttribute(string.Format("{0}.Image", bnt));
                    tmpImgaeOver = this.Core.GetAttribute(string.Format("{0}.ImageOver", bnt));
                    tmpImgaePressed = this.Core.GetAttribute(string.Format("{0}.ImagePressed", bnt));

                    actionButton.Add(new ActionButton()
                    {
                        Name = bnt,
                        Text = this.Core.GetAttribute(string.Format("{0}.Text", bnt)),
                        Visibility = iswindowscontrol.Contains(bnt) ? Visibility.Collapsed : Visibility.Visible,
                        IsWindowsControl = iswindowscontrol.Contains(bnt),
                        Image = !tmpImgae.IsNullOrEmpty()
                                                    ? (tmpImgae.Contains("http")
                                                        ? await this.Core.GetAttributeMediaWebImage(new Uri(tmpImgae))
                                                        : await this.Core.GetAttributeMediaWebImage(string.Format("{0}.Image", bnt)))
                                                    : null,
                        ImageOver = !tmpImgaeOver.IsNullOrEmpty()
                                                    ? (tmpImgaeOver.Contains("http")
                                                        ? await this.Core.GetAttributeMediaWebImage(new Uri(tmpImgaeOver))
                                                        : await this.Core.GetAttributeMediaWebImage(string.Format("{0}.ImageOver", bnt)))
                                                    : null,
                        ImagePressed = !tmpImgaePressed.IsNullOrEmpty()
                                                    ? (tmpImgaePressed.Contains("http")
                                                        ? await this.Core.GetAttributeMediaWebImage(new Uri(tmpImgaePressed))
                                                        : await this.Core.GetAttributeMediaWebImage(string.Format("{0}.ImagePressed", bnt)))
                                                    : null
                    }
                    );
                }

                this.ActionButtons = actionButton;
            }
            catch (Exception ex)
            {
                DiagnosticsTool.MyTrace(ex);
            }
            finally
            {
                (this.Core as IAction).ControlAction(this, "SetActionButtons.Complete");
                this.IsSetActionButtons = false;
            }
        }

        public async void SetTopMenu(DataTable dataTable)
        {
            ComboxItem comboxItem1;
            ComboxItem comboxItem2;
            int cntException;
            List<ComboxItem> comboxItems;
            decimal defaultMenuID;

            comboxItems = new List<ComboxItem>();

            cntException = 0;
            comboxItem2 = null;

            try
            {
                defaultMenuID = this.Core.GetAttributeDecimal("DefaultMenuID");
            }
            catch (Exception ex)
            {
                DiagnosticsTool.MyTrace(ex);
                defaultMenuID = -1;
            }

            foreach (DataRow dataRow in dataTable.Rows)
            {
                try
                {
                    comboxItem1 = new ComboxItem()
                    {
                        MenuID = (decimal)dataRow["MENU_ID"],
                        ParentMenuID = (decimal)dataRow["PARENT_MENU_ID"],
                        Name = (string)dataRow["NAME"],
                        Description = (string)dataRow["DESCRIPTION"],
                        Image = dataRow["IMAGE_URL1"] != DBNull.Value && (string)dataRow["IMAGE_URL1"] != ""
                                                    ? (((string)dataRow["IMAGE_URL1"]).Contains("http")
                                                        ? await this.Core.GetAttributeMediaWebImage(new Uri((string)dataRow["IMAGE_URL1"]))
                                                        : null)
                                                    : null
                    };

                    if (comboxItem1 != null)
                    {
                        comboxItems.Add(comboxItem1);

                        if (defaultMenuID == comboxItem1.MenuID)
                            comboxItem2 = comboxItem1;

                        if (dataTable.Rows.Count == (comboxItems.Count + cntException))
                        {
                            this.ComboxMenuItem = comboxItems;

                            if (comboxItem2 != null)
                                this.ComboxMenuItemSelected = comboxItem2;
                        }
                    }
                }
                catch (Exception ex)
                {
                    cntException += 1;
                }

            }


            //comboxItem1 = new ComboxItem()
            //{
            //    Text = "생산관리",
            //    Image = new BitmapImage(new Uri("/Atomus.Windows.Controls.Toolbar.ModernToolbar;component/Image/icon-menu-02-pm.png", UriKind.Relative))
            //};

            //if (comboxItem1 != null && comboxItem1.Image != null)
            //    this.ComboxMenuItem.Add(comboxItem1);

            //comboxItem1 = new ComboxItem()
            //{
            //    Text = "히스토리안",
            //    Image = new BitmapImage(new Uri("/Atomus.Windows.Controls.Toolbar.ModernToolbar;component/Image/icon-menu-03-history.png", UriKind.Relative))
            //};

            //if (comboxItem1 != null && comboxItem1.Image != null)
            //    this.ComboxMenuItem.Add(comboxItem1);

            //comboxItem1 = new ComboxItem()
            //{
            //    Text = "방사제조",
            //    Image = await this.Core.GetAttributeMediaWebImage("Image1")
            //};

            //if (comboxItem1 != null)
            //    this.ComboxMenuItem.Add(comboxItem1);

            //comboxItem1 = new ComboxItem()
            //{
            //    Text = "물성추적",
            //    Image = new BitmapImage(new Uri("/Atomus.Windows.Controls.Toolbar.ModernToolbar;component/Image/icon-menu-05.png", UriKind.Relative))
            //};

            //if (comboxItem1 != null && comboxItem1.Image != null)
            //    this.ComboxMenuItem.Add(comboxItem1);
        }

        public void SetActionButton(string action, bool isEnabled)
        {
            if (this.actionButtons == null)
                return;

            if (this.actionButtons[0].Name == action)
                foreach (ActionButton actionButton in this.actionButtons)
                    if (actionButton.IsWindowsControl)
                        actionButton.Visibility = Visibility.Collapsed;

            foreach (ActionButton actionButton in this.actionButtons)
            {
                if (actionButton.Name == action && actionButton.IsWindowsControl)
                    if (isEnabled)
                    {
                        actionButton.Visibility = Visibility.Visible;
                    }
                    else
                        actionButton.Visibility = Visibility.Collapsed;
            }

            var aa = from sel in this.actionButtons
                     where sel.IsWindowsControl
                     && sel.Visibility == Visibility.Visible
                     select sel;

            if (aa.Count() > 0)
            {
                aa = from sel in this.actionButtons
                         where sel.Name == "Close"
                         select sel;

                if (aa != null && aa.Count() == 1)
                    aa.ToArray()[0].Visibility = Visibility.Visible;
            }



        }
        #endregion

        #region IO
        internal void RibbonButtonProcess(object parameter)
        {
            if (parameter != null && parameter is string)
                (this.Core as IAction).ControlAction(this, (string)parameter, null);

            //if (parameter == null)
            //    this.WindowsMessageBoxShow(Application.Current.Windows[0], "Test");
            //else
            //    this.WindowsMessageBoxShow(Application.Current.Windows[0], string.Format("Test : {0}", parameter));
        }
        #endregion

        #region ETC
        #endregion
    }
}