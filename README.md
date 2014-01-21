perils-of-npc
=============

The perils of NPC. What do I mean by that? I'm glad you asked.

NPC primarily referring to [INotifyPropertyChanged]
(http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged.aspx).

By extension I suppose you could include [INotifyPropertyChanging]
(http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanging.aspx)
in the same discussion, although that is something new in .NET 4 that I know
of, at least as concerns Model-aware systems that have been around awhile,
since at least [.NET 3.5](http://en.wikipedia.org/wiki/.NET_Framework).

You could extend this discussion into areas such as [ObservableCollections]
(http://msdn.microsoft.com/en-us/library/ms668604.aspx), which are powerful
and useful in their own right, whenever notifying lists of things is required.
However, for purposes of developing the topic, I will leave that issue aside
as an exercise for the reader to explore on his or her own.

I'll just mention that I have found INotifyPropertyChanged to be far more
commonly recognized by NPC-aware components and component-vendors. You've
probably used one or two of them, if you haven't already rolled your own:
[Telerik](http://www.telerik.com/), [DevExpress](http://www.devexpress.com/),
etc. For purposes of developing the topic I will also leave
INotifyPropertyChanging as a follow on exercise for the reader to explore
independently.

I will also be edging into common currently used design patterns such as
[MVC](http://en.wikipedia.org/wiki/Model_view_controller) (Model-View-Controller)
and [MVVM](http://en.wikipedia.org/wiki/Model_View_ViewModel) (Model-View-ViewModel),
mainly focusing on the Model portion, potentially extending into View and/or
ViewModel concerns, of a sort if you are dealing with MVC. The concepts are
similar enough to make it a worthwhile Model-centric topic.

Most of the initial concepts will mostly likely be familiar to most of you
who have been around the .NET framework awhile and done anything even
remotely serious with a model framework. Some of the concepts later are new
with .NET 4.5, which was part and partial my motivation for wanting to develop
a topic in this area.

Expect that this will be a work in progress and I will pick it up here...

## Opening Trick

For my opening trick, I will start with a simple model, **Quantity**.

```C#
class Quantity
{
    public double Value { get; set; }

    public string Unit { get; set; }
}
```

That's it to start with. Obviously, the model is not communicating anything
yet. It is a simple POCO, **Quantity** with a **Value** and a **Unit**. Pretty
straightforward. You would not plug this into any views or view models and
expect updates to occur auto-magically. But wait, it gets more interesting.

## A Simple Notification

We will add "simple" NPC to the equation now. For that, I typically introduce
a backing field which the property uses as a reference.

```C#
class Quantity : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        if (PropertyChanged == null) return;
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

    private double _value = default(double);

    public double Value
    {
        get { return _value; }
        set
        {
            _value = value;
            OnPropertyChanged("Value");
        }
    }

    private string _unit = string.Empty;

    public string Unit
    {
        get { return _unit; }
        set
        {
            _unit = value;
            OnPropertyChanged("Unit");
        }
    }
}
```

### Unit Testing

Let's add some unit testing as well in order so that we can demonstrate some
of these issues starting to work themselves out. Let's assume that the test
fixture setup occurs and we have a Quantity instance available in the test
case.

```C#
[Test]
public void Test_That_Value_PropertyChanged()
{
    var changed = false;

    PropertyChangedEventHandler handler = (s, e) =>
    {
        Assert.That(s, Is.SameAs(_quantity));
        Assert.That(e.PropertyName, Is.EqualTo("Value"));
        changed = true;
    };

    try
    {
        _quantity.PropertyChanged += handler;

        _quantity.Value = 1d;

        Assert.That(changed, Is.True);
    }
    finally
    {
        _quantity.PropertyChanged -= handler;
    }
}
```

Per se, it does not matter that we assert the actual changed value; rather,
only that we know it changed. We can perform a similar verification for the
**Unit** property.

Note, this is testing one path through the **PropertyChanged** sequence.
Depending on your unit testing concerns, you would want to consider the other
paths, like **Unit** would not necessarily change because Value changed; use
cases like that. We will not worry about that part unless we need to for
purposes of discussion.

In practice I would take a few minutes to abstract delegated actions,
functions, helpers, and so on to reduce the typing overhead. But for purposes
of this discussion, this is sufficient. I will go ahead and do this to save
some typing prior to committing.

## Glaring Opportunities

One glaring opportunity is that **PropertyChanged** is being raised every time
a property value is *set*. Rather, we might want to verify whether the
property value *actually changes*.

In the case of the **Value** property, we do something like this:

```C#
public double Value
{
    get { return _value; }
    set
    {
        if (_value == value) return;
        _value = value;
        OnPropertyChanged("Value");
    }
}
```

Notice the simple check whether the value should change. The exact nature of
the comparison may vary from use case to use case: if precision comparisons
are necessary, whether strings or object instances are involved, and so on.

This is a start, but as you can see, when moderately complex model objects
are involved, this pattern starts to become quite cumbersome. But wait, that's
not all. I will continue to flesh out the perils I've observed.

Not only that but you must also hard-code the name of the property in the call
to **OnPropertyChanged**. I'm not an especially big fan of the OnSuchAndSuch
naming convention to begin with; RaiseSuchAndSuch seems more action-oriented,
where as On is more like a predicate.

## Helpers By Half-Measures

Up until now the projects have targeted [.NET 4]
(http://msdn.microsoft.com/en-us/library/w0x726c2%28v=vs.100%29.aspx).
However, with [.NET 4.5]
(http://msdn.microsoft.com/en-us/library/w0x726c2%28v=vs.110%29.aspx),
Microsoft has introduced an Attribute called [CallerMemberNameAttribute]
(http://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.callermembernameattribute%28v=vs.110%29.aspx),
which you use to decorate a name parameter such as to our
**OnPropertyChanged**. It might be helpful for things like logging helpers,
and things of this nature as well.

Allow me to fold this into the mix as a demonstration.

```C#
private void OnPropertyChanged([CallerMemberName] string propertyName = null)
{
    if (PropertyChanged == null) return;
    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
}

//...

public double Value
{
    get { return _value; }
    set
    {
        if (_value == value) return;
        _value = value;
        OnPropertyChanged();
    }
}
```

We should now be able to run our unit tests, and indeed verify that this still
"works", at least insofar as we've implemented the solution thus far.

But, why do I mention this in a subject such as half-measures? I am going to
suggest that that's just what this Attribute is: a half-measure.

Imagine we had a use case where we wanted to derive some calculated value from
a Value. Wouldn't we also want to notify that property has changed? In a sane
model, of course we would.

```C#
public double DerivedValue
{
    get { return Value*2.5d; }
}

//...

public double Value
{
    get { return _value; }
    set
    {
        if (_value == value) return;
        _value = value;
        OnPropertyChanged();
        OnPropertyChanged("DerivedValue");
    }
}
```

However, with the property decorated such as it is, it fulfills one fairly
narrow use case. We could call OnPropertyChanged again with the
**DerivedValue** property name, but you see we've come full circle.
We must still hard-code a property name for the derived properties.

I will suggest extremely limited to no real, lasting value has been added by
this Attribute decoration. But instead of leaving the discussion stranded on
a criticism such as this, I will suggest that with not much more effort, there
are already mechanisms available that can facilitate solving this issue.

I updated the unit tests accordingly with this step in the discussion.

## Nailing Down the Property Name

What I really want is a base-class, with a helper method, to help capture this
and nearby concerns. However, before we get to that part, I want to address the
property name issue.

Will pick up with this exercise later...
