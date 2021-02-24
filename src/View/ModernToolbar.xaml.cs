using Atomus.Control;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Atomus.Windows.Controls.Toolbar
{
    /// <summary>
    /// UserControl1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ModernToolbar : UserControl, IAction
    {
        private AtomusControlEventHandler beforeActionEventHandler;
        private AtomusControlEventHandler afterActionEventHandler;

        #region Init
        public ModernToolbar()
        {
            InitializeComponent();

            this.DataContext = new ViewModel.ModernToolbarViewModel(this);
        }
        #endregion

        #region Dictionary
        #endregion

        #region Spread
        #endregion

        #region IO
        object IAction.ControlAction(ICore sender, AtomusControlArgs e)
        {
            //AtomusControlEventArgs atomusControlEventArgs;

            try
            {
                this.beforeActionEventHandler?.Invoke(this, e);

                switch (e.Action)
                {
                    case "UserToolbarButton.Remove":
                        this.ToolbarButtonsRemove(sender);
                        return true;

                    case "UserToolbarButton.Add":
                        this.ToolbarButtonsAdd(sender);
                        return true;

                    case "Menu.TopMenu":
                        if (e.Value != null && e.Value is DataTable)
                            (this.DataContext as ViewModel.ModernToolbarViewModel).SetTopMenu(e.Value as DataTable);

                        return true;

                    case "Menu.TopMenuSelected":
                        return true;

                    //case "Toolbar.Background":
                    //    return this.ribbon.Background;

                    //case "Toolbar.BorderBrush":
                    //    return this.ribbon.BorderBrush;

                    //case "Toolbar.BorderThickness":
                    //    return this.ribbon.BorderThickness;


                    default:
                        if (e.Action.StartsWith("Action."))
                            foreach (RibbonButton ribbonButton in this.FindVisualChildren<RibbonButton>())
                            {
                                if (ribbonButton.Name.Equals(e.Action.Split('.')[1]))
                                {
                                    ribbonButton.IsEnabled = e.Value.Equals("Y");
                                    return true;
                                }
                            }

                        if (e.Action.StartsWith("Action."))
                            (this.DataContext as ViewModel.ModernToolbarViewModel).SetActionButton(e.Action.Split('.')[1], e.Value.Equals("Y"));

                        return false;

                }
            }
            finally
            {
                if (!new string[] { "UserToolbarButton.Remove", "UserToolbarButton.Add", "Menu.TopMenu" }.Contains(e.Action))
                    this.afterActionEventHandler?.Invoke(this, e);
            }
        }
        #endregion

        #region Event
        event AtomusControlEventHandler IAction.BeforeActionEventHandler
        {
            add
            {
                this.beforeActionEventHandler += value;
            }
            remove
            {
                this.beforeActionEventHandler -= value;
            }
        }
        event AtomusControlEventHandler IAction.AfterActionEventHandler
        {
            add
            {
                this.afterActionEventHandler += value;
            }
            remove
            {
                this.afterActionEventHandler -= value;
            }
        }
        #endregion

        #region ETC
        #endregion

        System.Windows.Controls.Ribbon.RibbonMenuItem ribbonMenuItem;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //(ribbon.ContextMenu.Items[0] as System.Windows.Controls.Ribbon.RibbonMenuItem).Click -= DefaultToolbar_Click;
            //(ribbon.ContextMenu.Items[0] as System.Windows.Controls.Ribbon.RibbonMenuItem).Click += DefaultToolbar_Click;

            if (this.ribbonMenuItem == null)
            {
                this.ribbonMenuItem = new System.Windows.Controls.Ribbon.RibbonMenuItem()
                {
                    Header = string.Format("{0}(_V)", "빠른실행 표시".Translate("")),
                    IsCheckable = true,
                    IsChecked = true
                };

                this.ribbonMenuItem.Click += RibbonMenuItem_Click;
                //ribbon.ContextMenu.Items.Add(this.ribbonMenuItem);
            }

            try
            {
                this.SetRibbon();
            }
            catch (Exception ex)
            {
                (this).WindowsMessageBoxShow(Application.Current.Windows[0], ex);
            }
        }


        private async void SetRibbon()
        {
            string tmp;
            string[] ribbonTabs;
            RibbonTab ribbonTab;
            RibbonGroup ribbonGroup;
            string[] ribbonGroups;
            RibbonButton ribbonButton;
            string[] ribbonButtons;

            tmp = this.GetAttribute("Ribbon.RibbonTabs");
            if (!tmp.IsNullOrEmpty())
            {
                ribbonTabs = tmp.Split(',');

                foreach (string tabname in ribbonTabs)
                {
                    ribbonTab = new RibbonTab()
                    {
                        KeyTip = this.GetAttribute(string.Format("Ribbon.RibbonTab_{0}.KeyTip", tabname)),
                        Header = this.GetAttribute(string.Format("Ribbon.RibbonTab_{0}.Header", tabname)),
                        Name = tabname
                    };

                    //this.ribbon.Items.Add(ribbonTab);


                    tmp = this.GetAttribute(string.Format("Ribbon.RibbonTab_{0}.RibbonGroups", tabname));
                    if (!tmp.IsNullOrEmpty())
                    {
                        ribbonGroups = tmp.Split(',');

                        foreach (string groupname in ribbonGroups)
                        {
                            ribbonGroup = new RibbonGroup()
                            {
                                KeyTip = this.GetAttribute(string.Format("Ribbon.RibbonGroup_{0}.KeyTip", groupname)),
                                Header = this.GetAttribute(string.Format("Ribbon.RibbonGroup_{0}.Header", groupname))
                            };

                            ribbonTab.Items.Add(ribbonGroup);


                            tmp = this.GetAttribute(string.Format("Ribbon.RibbonGroup_{0}.RibbonButtons", groupname));
                            if (!tmp.IsNullOrEmpty())
                            {
                                ribbonButtons = tmp.Split(',');

                                foreach (string buttonname in ribbonButtons)
                                {
                                    ribbonButton = new RibbonButton()
                                    {
                                        KeyTip = this.GetAttribute(string.Format("Ribbon.RibbonButton_{0}.KeyTip", buttonname)),
                                        Label = this.GetAttribute(string.Format("Ribbon.RibbonButton_{0}.Label", buttonname)),
                                        LargeImageSource = await this.GetAttributeMediaWebImage(string.Format("Ribbon.RibbonButton_{0}.LargeImageSource", buttonname)),
                                        SmallImageSource = await this.GetAttributeMediaWebImage(string.Format("Ribbon.RibbonButton_{0}.SmallImageSource", buttonname)),
                                        Command = new MVVM.DelegateCommand(
                                            () => { (this.DataContext as ViewModel.ModernToolbarViewModel).RibbonButtonProcess(buttonname); }, () => { return true; }
                                            ),
                                        Tag = buttonname,
                                        CanAddToQuickAccessToolBarDirectly = true,
                                        Name = buttonname
                                    };

                                    ribbonGroup.Items.Add(ribbonButton);
                                }
                            }

                        }
                    }
                }
            }

        }


        private void ToolbarButtonsRemove(ICore core)
        {
            //RibbonGroup ribbonGroup;

            //if (Config.Client.GetAttribute(core, "ToolbarButtons") == null)
            //    return;

            //if (!(Config.Client.GetAttribute(core, "ToolbarButtons") is RibbonGroup))
            //    return;

            //ribbonGroup = (RibbonGroup)Config.Client.GetAttribute(core, "ToolbarButtons");

            //foreach (RibbonTab ribbonGroup1 in this.FindVisualChildren<RibbonTab>())
            //{
            //    if (ribbonGroup1.Name.Equals("System"))
            //    {
            //        ribbonGroup1.Items.Remove(ribbonGroup);
            //        return;
            //    }
            //}
        }
        private async void ToolbarButtonsAdd(ICore core)
        {
            //string tmp;
            //RibbonGroup ribbonGroup;
            //RibbonButton ribbonButton;
            //string[] ribbonButtons;

            //if (Config.Client.GetAttribute(core, "ToolbarButtons") == null)
            //{
            //    ribbonGroup = new RibbonGroup()
            //    {
            //        Header = core.GetAttribute("NAME")
            //    };

            //    tmp = core.GetAttribute("ToolbarButtonItems");

            //    if (tmp.IsNullOrEmpty())
            //        return;

            //    ribbonButtons = tmp.Split(',');

            //    foreach (string buttonname in ribbonButtons)
            //    {
            //        ribbonButton = new RibbonButton()
            //        {
            //            Label = core.GetAttribute(string.Format("{0}.Text", buttonname)),
            //            LargeImageSource = await core.GetAttributeMediaWebImage(string.Format("{0}.ImageOn", buttonname)),
            //            SmallImageSource = await core.GetAttributeMediaWebImage(string.Format("{0}.Image", buttonname)),
            //            Command = new MVVM.DelegateCommand(
            //                () => { (this.DataContext as ViewModel.ModernToolbarViewModel).RibbonButtonProcess(buttonname); }, () => { return true; }
            //                ),
            //            Tag = buttonname,
            //            CanAddToQuickAccessToolBarDirectly = false,
            //            Name = buttonname
            //        };

            //        ribbonGroup.Items.Add(ribbonButton);
            //    }

            //    Config.Client.SetAttribute(core, "ToolbarButtons", ribbonGroup);
            //}


            //ribbonGroup = (RibbonGroup)Config.Client.GetAttribute(core, "ToolbarButtons");

            //foreach (RibbonTab ribbonGroup1 in this.FindVisualChildren<RibbonTab>())
            //{
            //    if (ribbonGroup1.Name.Equals("System"))
            //    {
            //        ribbonGroup1.Items.Insert(1, ribbonGroup);
            //        return;
            //    }
            //}
        }


        private void RibbonMenuItem_Click(object sender, RoutedEventArgs e)
        {
            RibbonMenuItem ribbonMenuItem;

            ribbonMenuItem = (sender as RibbonMenuItem);

            //ribbonMenuItem.IsChecked = !ribbonMenuItem.IsChecked;

            //ribbon.ShowQuickAccessToolBarOnTop = true;

            //if (ribbonMenuItem.IsChecked)
            //    ribbon.Margin = new Thickness(0, 0, 0, 0);
            //else
            //    ribbon.Margin = new Thickness(0, -22, 0, 0);
        }

        private void DefaultToolbar_Click(object sender, RoutedEventArgs e)
        {
            //if (ribbon.ShowQuickAccessToolBarOnTop)
            //{
            //    ribbonQuickAccessToolBar.Margin = new Thickness(0, 0, 0, 0);
            //    ribbon.Margin = new Thickness(0, -22, 0, 0);
            //}
            //else
            //{
            //    ribbonQuickAccessToolBar.Margin = new Thickness(0, 2.2, 0, 0);
            //    ribbon.Margin = new Thickness(0, 0, 0, 0);
            //}

            ribbonMenuItem.IsChecked = true;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox;

            listBox = e.Source as ListBox;

            listBox.SelectedItem = null;
        }
    }
}
