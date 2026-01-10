using Avalonia.Controls;
using Avalonia.Threading;
using Avalonia.Xaml.Interactivity; // Behavior的命名空间
using System.Collections.Specialized;
using Avalonia.Input;

namespace AvaloniaApplication1.Behaviors;

public class AutoScrollToEndBehavior : Behavior<ScrollViewer>
{
    private INotifyCollectionChanged? _collection;
    private bool _isProgrammaticScroll; // 用于识别是程序自身在滚动的标志
    private bool _userHasScrolledUp;    // 用于跟踪用户是否已向上滚动

    /// <summary>
    /// 当Behavior附加到控件时调用。
    /// </summary>
    protected override void OnAttached()
    { 
        base.OnAttached(); 
        if (AssociatedObject != null) 
        { 
            // 等待ScrollViewer加载完毕后再执行逻辑
            AssociatedObject.Loaded += OnScrollViewerLoaded;
            AssociatedObject.ScrollChanged += OnScrollChanged;
            AssociatedObject.PointerExited += OnPointerExited;
        }
    }

    /// <summary>
    /// 当Behavior从控件分离时调用，用于清理。
    /// </summary>
    protected override void OnDetaching()
    {
        base.OnDetaching();
        if (AssociatedObject != null)
        {
            AssociatedObject.Loaded -= OnScrollViewerLoaded;
            AssociatedObject.ScrollChanged -= OnScrollChanged;
            AssociatedObject.PointerExited -= OnPointerExited;
        }
        // 取消对集合变更的订阅，防止内存泄漏
        if (_collection != null)
        {
            _collection.CollectionChanged -= OnCollectionChanged;
        }
    }

    private void OnScrollViewerLoaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        // 尝试获取ScrollViewer内部的ItemsControl
        if (AssociatedObject?.Content is ItemsControl itemsControl && itemsControl.Items is INotifyCollectionChanged collection)
        {
            _collection = collection;
            _collection.CollectionChanged += OnCollectionChanged;
            ScrollToEnd(); // 初始加载时滚动一次
        }
    }

    /// <summary>
    /// 核心逻辑：当用户滚动时触发。
    /// </summary>
    private void OnScrollChanged(object? sender, ScrollChangedEventArgs e)
    {
        // 如果是程序自己触发的滚动，则重置标志并直接返回
        if (_isProgrammaticScroll)
        {
            _isProgrammaticScroll = false;
            return;
        }
        
        // 否则，一定是用户手动滚动。我们据此更新用户状态。
        if (AssociatedObject != null)
        {
            // 检查滚动条是否在底部 (使用一个小的容差)
            bool isAtBottom = AssociatedObject.ScrollBarMaximum.Y - AssociatedObject.Offset.Y < 3.0;
            _userHasScrolledUp = !isAtBottom;
        }
    }

    /// <summary>
    /// 当绑定的集合（如ObservableCollection）发生变化时触发。
    /// </summary>
    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        // 只在新项目添加时，并且用户没有向上滚动时，才执行自动滚动
        if (e.Action == NotifyCollectionChangedAction.Add && !_userHasScrolledUp)
        {
            ScrollToEnd();
        }
    }
    
    private void OnPointerExited(object? sender, PointerEventArgs e)
    {
        // 当鼠标离开控件区域时，重置用户滚动状态。
        _userHasScrolledUp = false;
    }
        
        
    /// <summary>
    /// 执行滚动到底部的操作。
    /// </summary>
    private void ScrollToEnd()
    {
        Dispatcher.UIThread.Post(() =>
        {
            if (AssociatedObject != null)
            {
                // 在执行滚动之前，先“举手”告诉OnScrollChanged这是程序行为
                _isProgrammaticScroll = true;
                AssociatedObject.ScrollToEnd();
            }
        });
    }
        
        
}