/*
Created by Bittiez and released on RunUO Forums.

Instructions:

Find and open the file Scripts\Items\Misc\PlayerVendorDeed.cs

Directly under this line "BaseHouse house = BaseHouse.FindHouseAt( from );" add the following:

#region VendorTile
Sector sector = from.Map.GetSector(from.Location);
foreach (Item i in sector.Items)
{
    if (i is VendorTile && i.Location.X == from.Location.X && i.Location.Y == from.Location.Y)
    {
        Mobile v = new PlayerVendor(from, house);

        v.Direction = from.Direction & Direction.Mask;
        v.MoveToWorld(from.Location, from.Map);

        v.SayTo(from, 503246); // Ah! it feels good to be working again.

        this.Delete();
        return;
    }
}
#endregion
*/

namespace Server.Items
{
    public class VendorTile : Item
    {
        [Constructable]
        public VendorTile() : base(0x3F08)
        {
            Name = "a vendor tile";
            Movable = false;
        }

        public override bool Decays { get { return false; } }

        public VendorTile(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }
    }
}
