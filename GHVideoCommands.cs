using GHVideo.Core;
using GHVideoApp.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GHVideoApp
{
    public static class GHVideoCommands
    {

        public static void ClearPropertyCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ComboBox? comboBox = e.Source as ComboBox;

            if (comboBox != null)
                comboBox.SelectedIndex = -1;
        }
        public static void ClearPropertyCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;

            ComboBox? comboBox = e.Source as ComboBox;

            if (comboBox != null && comboBox.SelectedIndex >= 0)
                e.CanExecute = true;
        }

        #region 演员 Commands
        public static readonly RoutedUICommand ActorAdd = new RoutedUICommand(
            "添加演员",
            "ActorAdd",
            typeof(FrameworkElement),//owner type
            new InputGestureCollection()//gesture collection
            {
               new KeyGesture(Key.F1, ModifierKeys.Alt)
            });

        public static readonly RoutedUICommand ActorClear = new RoutedUICommand(
            "清除演员",
            "ActorClear",
            typeof(FrameworkElement),//owner type
            new InputGestureCollection()//gesture collection
            {
               new KeyGesture(Key.F2, ModifierKeys.Alt)
            });



        public static void AddActorCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var doc = (ActorDocument)e.Parameter;
            var win = new WindowAddActor();

            if (win.ShowDialog() == true)
            {
                doc.ActorCollection.Add(win.Current);
                doc.SaveGH();
            }
        }

        public static void ClearActorCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ComboBox actorComboBox = e.Source as ComboBox;

            if (actorComboBox != null)
                actorComboBox.SelectedIndex = -1;
        }

        public static void ClearActorCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            ComboBox actorComboBox = e.Source as ComboBox;

            if (actorComboBox != null && actorComboBox.SelectedIndex >= 0)
                e.CanExecute = true;
        }

        #endregion

        public static readonly RoutedUICommand FanAdd = new RoutedUICommand(
            "添加番号",
            "FanAdd",
            typeof(FrameworkElement),//owner type
            new InputGestureCollection()//gesture collection
            {
               new KeyGesture(Key.F3, ModifierKeys.Alt)
            });



        #region 标签 Commands
        public static readonly RoutedUICommand TagAdd = new RoutedUICommand(
            "添加标签",
            "TagAdd",
            typeof(FrameworkElement),//owner type
            new InputGestureCollection()//gesture collection
            {
               new KeyGesture(Key.F5, ModifierKeys.Alt)
            });

        public static void AddTagCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var doc = (AllPropertyDocument)e.Parameter;

            if (doc != null && doc.Tags != null)
            {
                var win = new WindowAddProperty(doc.Tags);

                if (win.ShowDialog() == true)
                {
                    doc.TagCollection.Add(win.NewProperty);
                    doc.SaveGH();
                }
            }
        }


        public static readonly RoutedUICommand TagClear = new RoutedUICommand(
            "清除标签",
            "TagClear",
            typeof(FrameworkElement),//owner type
            new InputGestureCollection()//gesture collection
            {
               new KeyGesture(Key.F5, ModifierKeys.Control)
            });

        #endregion

        public static readonly RoutedUICommand TypeAdd = new RoutedUICommand(
            "添加类型",
            "TypeAdd",
            typeof(FrameworkElement),//owner type
            new InputGestureCollection()//gesture collection
            {
               new KeyGesture(Key.F7, ModifierKeys.Alt)
            });
    }
}
