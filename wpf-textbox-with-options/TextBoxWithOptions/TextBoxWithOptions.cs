using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextBoxWithOptions
{
    public class TextBoxWithOptions : Control
    {
        #region プロパティ
        public string DisplayMemberPath { get; set; }
        public string FilterMemberPath { get; set; }

        public string[] FilterMemberPaths { get; set; }

        #endregion

        #region 依存関係プロパティ
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                "ItemsSource",
                typeof(IEnumerable),
                typeof(TextBoxWithOptions), 
                new PropertyMetadata(null, ItemsSourcePropertyChanged));
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(
                ItemsSourceProperty, value); }
        }

        private static void ItemsSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxWithOptions tx = d as TextBoxWithOptions;
            if (e.NewValue != null)
            {
                tx.ListOptions.ItemsSource = e.NewValue as IEnumerable;
            }
        }
        #endregion


        public TextBoxWithOptions()
        {
            //this.DataContext = new TextBoxWithOptionsViewModel();
        }


        static TextBoxWithOptions()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBoxWithOptions), new FrameworkPropertyMetadata(typeof(TextBoxWithOptions)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.TextKeyword = this.Template.FindName("textKeyword", this) as TextBox;
            this.PopupHistory = this.Template.FindName("popupOptions", this) as Popup;
            this.ListOptions = this.Template.FindName("listOptions", this) as ListBox;

            this.ListOptions.ItemsSource = this.ItemsSource;


            this.ListOptions.PreviewKeyDown += ListOptions_PreviewKeyDown;


            this.TextKeyword.GotKeyboardFocus += TextKeyword_GotKeyboardFocus;
            this.TextKeyword.LostKeyboardFocus += TextKeyword_LostKeyboardFocus;
            this.TextKeyword.PreviewKeyDown += TextKeyword_PreviewKeyDown;


            this.Win = Window.GetWindow(this);
            this.Win.LocationChanged += Win_LocationChanged;
            this.Win.SizeChanged += Win_SizeChanged;
        }

        private void ListOptions_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //this.PopupHistory.IsOpen = false;
                //SampleData data = this.ListOptions.SelectedItem as SampleData;
                //this.TextKeyword.Text = data.Name;

                //this.TextKeyword.Focus();
                //this.TextKeyword.Select(this.TextKeyword.Text.Length, 0);
            }
        }

        private void TextKeyword_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                this.PopupHistory.IsOpen = true;
                this.ListOptions.Focus();
            }
        }

        private void Win_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.PopupHistory.IsOpen = false;
        }

        private void Win_LocationChanged(object sender, EventArgs e)
        {
            this.PopupHistory.IsOpen = false;
        }

        private void TextKeyword_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            //this.PopupHistory.IsOpen = false;
        }

        private void TextKeyword_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            this.PopupHistory.IsOpen = true;
        }


        public TextBox TextKeyword { get; set; }
        public Popup PopupHistory { get; set; }
        public ListBox ListOptions { get; set; }

        public Window Win { get; set; }

    }
}
