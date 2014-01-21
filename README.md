perils-of-npc
=============

The perils of NPC. What do I mean by that? I'm glad you asked.

NPC primarily referring to [INotifyPropertyChanged]
(http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged.aspx).

By extension I suppose you could include [INotifyPropertyChanging]
(http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanging.aspx)
in the same discussion, although that is something new in .NET 4 that I know of, at least
as concerns Model-aware systems that have been around awhile, since at least [.NET 3.5]
(http://en.wikipedia.org/wiki/.NET_Framework).

You could extend this discussion into areas such as [ObservableCollections]
(http://msdn.microsoft.com/en-us/library/ms668604.aspx), which are powerful and useful in
their own right, whenever notifying lists of things is required. However, for purposes of
developing the topic, I will leave that issue aside as an exercise for the reader to
explore on his or her own.

I'll just mention that I have found INotifyPropertyChanged to be far more commonly
recognized by NPC-aware components and component-vendors. You've probably used one
or two of them, if you haven't already rolled your own: [Telerik](http://www.telerik.com/),
[DevExpress](http://www.devexpress.com/), etc. For purposes of developing the topic I will
also leave INotifyPropertyChanging as a follow on exercise for the reader to explore
independently.

I will also be edging into common currently used design patterns such as
[MVC](http://en.wikipedia.org/wiki/Model_view_controller) (Model-View-Controller)
and [MVVM](http://en.wikipedia.org/wiki/Model_View_ViewModel) (Model-View-ViewModel),
mainly focusing on the Model portion, potentially extending into View and/or ViewModel
concerns, of a sort if you are dealing with MVC. The concepts are similar enough to
make it a worthwhile Model-centric topic.

Most of the initial concepts will mostly likely be familiar to most of you who have
been around the .NET framework awhile and done anything even remotely serious with
a model framework. Some of the concepts later are new with .NET 4.5, which was part
and partial my motivation for wanting to develop a topic in this area.

Expect that this will be a work in progress and I will pick it up here...

# Opening Trick

For my opening trick, I will start with a simple model, **Quantity**.

```C#
class Quantity
{
    public double Value { get; set; }

    public string Unit { get; set; }
}
```

That's it to start with. Obviously, the model is not communicating anything yet.
It is a simple POCO, **Quantity** with a **Value** and a **Unit**. Pretty
straightforward. You would not plug this into any views or view models and expect
updates to occur auto-magically. But wait, it gets more interesting.
