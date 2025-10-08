using Godot;
using System;

using static Ids;

[GlobalClass]
public partial class RoomData : Resource
{
    [Export]
    public LocationId Location;
    [Export]
    public RoomId Type;
}
