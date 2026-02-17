using ChipMasters.GodotWrappers;
using Godot;

namespace ChipMasters.Util
{
    /// <summary>
    /// Godot <see cref="Node"/> based <see cref="IPool{T}"/> that pools instantiations of a given scene. 
    /// 
    /// Class is abstract, as it must be extended with a type to be used in the editor(last I checked).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract partial class ANPrefabPool<T> : NNode, IPool<T>
        where T : class
    {
        [Export] private PackedScene? _prefab;

        private readonly IPool<T> _pool;



        /// <inheritdoc/>
        public ANPrefabPool()
        {
            _pool = new RPool<T>(PreparePrefab);
        } // end ctor

        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _prefab.AssertNotNull();
        } // end _Ready()



        private T PreparePrefab()
        {
            Node n = _prefab?.Instantiate() ?? throw new RNotReadyException();
            AddChild(n); // add to cause _Ready to be propagated
            RemoveChild(n); // remove to not obstrcut usage.
            return (T)(object)n;
        } // end PreparePrefab()



        /// <inheritdoc/>
        public T Get() => _pool.Get();

        /// <inheritdoc/>
        public void Release(T instance) => _pool.Release(instance);
    } // end class
} // end namespace