using ChipMasters.Cards;
using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays;
using ChipMasters.Resources;
using ChipMasters.User;
using Godot;

namespace ChipMasters.Menu
{
    /// <summary>
    /// <see cref="ICard"/> <see cref="IDisplay{T}"/> that skins a <see cref="MeshInstance3D"/>.
    /// </summary>
#warning N-R split possibly? Might be difficult considering the material setting.
    public partial class NMeshSkinCardDisplay : NNode, IDisplay<ICard>
    {
        [Export] private MeshInstance3D? _mesh;
        [Export] private int _backOverrideIndex;
        [Export] private int _frontOverrideIndex;
        [Export] private int _indicatorOverrideIndex;



        /// <inheritdoc/>
        public ICard? Display
        {
            get => _displaying;
            set
            {
                if (_displaying == value)
                    return;

                _displaying = value;
                RefreshDisplay();
            }
        } // end Card
        private ICard? _displaying;
        /// <inheritdoc/>
        public bool Visible { get; set; }




        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _mesh.AssertNotNull();

            RUser.INSTANCE.AssetSelection.OnCardSkinChanged += RefreshDisplay;
            RefreshDisplay();
        } // end _Ready

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            RUser.INSTANCE.AssetSelection.OnCardSkinChanged -= RefreshDisplay;
        } // end Dispose()



        private void RefreshDisplay()
        {
            _mesh!.SetSurfaceOverrideMaterial(_backOverrideIndex, SAssetUtil.GetMaterials(SAssetUtil.CARD_BACK_PATH).Material3D);
            _mesh.SetSurfaceOverrideMaterial(_frontOverrideIndex, SAssetUtil.GetMaterials(SAssetUtil.CARD_FRONT_PATH).Material3D);
            _mesh.SetSurfaceOverrideMaterial(_indicatorOverrideIndex, _displaying?.GetMaterials().Material3D);
        } // end RefreshDisplay()

    } // end class
} // end namespace
