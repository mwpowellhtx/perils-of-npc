perils-of-npc
=============

The perils of NPC. What do I mean by that? I'm glad you asked.

NPC primarily referring to [INotifyPropertyChanged]
(http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged.aspx).

By extension I suppose you could include [INotifyPropertyChanging]
(http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanging.aspx)
in the same discussion, although that is something new int .NET 4 that I know of, at least
as concerns Model-aware systems that have been around awhile, since at least [.NET 3.5]
(http://en.wikipedia.org/wiki/.NET_Framework).

I'll just mention that I have found INotifyPropertyChanged to be far more commonly
recognized by NPC-aware components and component-vendors. You've probably used one
or two of them, if you haven't already rolled your own: [Telerik](http://www.telerik.com/),
[DevExpress](http://www.devexpress.com/), etc.

I will also be edging into common currently used design patterns such as
[MVC](http://en.wikipedia.org/wiki/Model_view_controller) (Model-View-Controller)
and [MVVM](http://en.wikipedia.org/wiki/Model_View_ViewModel) (Model-View-ViewModel),
mainly focusing on the Model portion, potentially extending into View and/or ViewModel
concerns, of a sort if you are dealing with MVC. The concepts are similar enough to
make it a worthwhile Model-centric topic.

Expect that this will be a work in progress and I will pick it up here...
