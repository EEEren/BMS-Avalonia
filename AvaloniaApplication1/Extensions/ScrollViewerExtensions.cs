using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using System.Collections.Specialized;

namespace AvaloniaApplication1.Extensions // 请确保使用您自己的项目命名空间
{
    public static class ScrollViewerExtensions
    {
        // 1. 定义附加属性
        public static readonly AttachedProperty<bool> AutoScrollToEndProperty =
            AvaloniaProperty.RegisterAttached<ScrollViewer, bool>(
                "AutoScrollToEnd", 
                typeof(ScrollViewerExtensions)
            );

        // 2. 提供 Get 和 Set 访问器（这是必需的样板代码）
        public static bool GetAutoScrollToEnd(ScrollViewer scrollViewer)
        {
            return scrollViewer.GetValue(AutoScrollToEndProperty);
        }

        public static void SetAutoScrollToEnd(ScrollViewer scrollViewer, bool value)
        {
            scrollViewer.SetValue(AutoScrollToEndProperty, value);
        }

        // 3. 当属性值改变时，执行核心逻辑
        static ScrollViewerExtensions()
        {
            AutoScrollToEndProperty.Changed.AddClassHandler<ScrollViewer>(OnAutoScrollToEndChanged);
        }

        private static void OnAutoScrollToEndChanged(ScrollViewer scrollViewer, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue is true)
            {
                // 当附加属性被设置为True时，订阅内容变化事件
                scrollViewer.ScrollChanged += OnScrollViewerLoaded;
                // 如果内容是ItemsControl，则监听其集合的变化
                // if (scrollViewer.Content is ItemsControl itemsControl && itemsControl.Items is INotifyCollectionChanged collection)
                // {
                //     collection.CollectionChanged += (s, args) => ScrollToEnd(scrollViewer);
                // }
            }
            else
            {
                // 当附加属性被设置为False时，取消订阅以防止内存泄漏
                scrollViewer.ScrollChanged -= OnScrollViewerLoaded;
                // if (scrollViewer.Content is ItemsControl itemsControl && itemsControl.Items is INotifyCollectionChanged collection)
                // {
                //     // 注意：这里简单地移除了一个匿名委托，在复杂场景下可能需要更精细的管理
                //     // 但对于这个用例通常是足够的。
                // }
            }
        }
        
        // 一个辅助方法，确保总是在UI线程上滚动到底部
        private static void ScrollToEnd(ScrollViewer scrollViewer)
        {
             Dispatcher.UIThread.Post(() =>
             {
                 scrollViewer.ScrollToEnd();
             }, DispatcherPriority.Background);
        }

        // 当用户手动滚动时，我们可以选择性地禁用自动滚动
        // (这是一个可选的增强功能)
        // NEW: 当 ScrollViewer 完全加载后，再执行我们的设置逻辑
        private static void OnScrollViewerLoaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is not ScrollViewer scrollViewer) return;

            // 逻辑和之前一样，但现在它在正确的时机运行
            if (scrollViewer.Content is ItemsControl itemsControl && itemsControl.Items is INotifyCollectionChanged collection)
            {
                // 为现有内容立即滚动到底部
                ScrollToEnd(scrollViewer);
                
                // 订阅未来的集合变化
                collection.CollectionChanged += (s, args) =>
                {
                    if (args.Action == NotifyCollectionChangedAction.Add)
                    {
                        // ★★★ 核心改动在这里 ★★★
                        // 检查滚动条是否已经接近或位于底部。
                        // 使用一个小的容差（如 1.0）可以避免浮点数精度问题。
                        bool isAtBottom = scrollViewer.Offset.Y >= scrollViewer.ScrollBarMaximum.Y - 1.0;

                        // 只有当用户已经在底部时，才自动滚动。
                        if (isAtBottom)
                        {
                            ScrollToEnd(scrollViewer);
                        }
                    }
                };
            }
            
            // 可选：执行一次后就可以取消订阅 Loaded 事件，防止重复执行
            scrollViewer.Loaded -= OnScrollViewerLoaded;
        }
        
    }
}