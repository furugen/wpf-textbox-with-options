using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using wpf_textbox_width_options.Data;

namespace wpf_textbox_with_options
{
    /// <summary>
    /// このカスタム コントロールを XAML ファイルで使用するには、手順 1a または 1b の後、手順 2 に従います。
    ///
    /// 手順 1a) 現在のプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:wpf_textbox_with_options"
    ///
    ///
    /// 手順 1b) 異なるプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:wpf_textbox_with_options;assembly=wpf_textbox_with_options"
    ///
    /// また、XAML ファイルのあるプロジェクトからこのプロジェクトへのプロジェクト参照を追加し、
    /// リビルドして、コンパイル エラーを防ぐ必要があります:
    ///
    ///     ソリューション エクスプローラーで対象のプロジェクトを右クリックし、
    ///     [参照の追加] の [プロジェクト] を選択してから、このプロジェクトを参照し、選択します。
    ///
    ///
    /// 手順 2)
    /// コントロールを XAML ファイルで使用します。
    ///
    ///     <MyNamespace:TextBoxWithOptions/>
    ///
    /// </summary>
    public class TextBoxWithOptions : Control
    {

        public TextBoxWithOptions()
        {
            this.DataContext = new TextBoxWithOptionsViewModel();
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
                this.PopupHistory.IsOpen = false;
                SampleData data = this.ListOptions.SelectedItem as SampleData;
                this.TextKeyword.Text = data.Name;

                this.TextKeyword.Focus();
                this.TextKeyword.Select(this.TextKeyword.Text.Length, 0);
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
