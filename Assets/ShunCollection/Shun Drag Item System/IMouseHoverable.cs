namespace Shun_Drag_Item_System
{
    public interface IMouseHoverable
    {
        public bool IsHovering { get;}
        public void StartHover();

        public void EndHover();

    }
}