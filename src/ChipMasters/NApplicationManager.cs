using ChipMasters.GodotWrappers;
using Godot;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChipMasters.IO
{
    /// <summary>
    /// Singletone godot <see cref="Node"/> that handles overall game application logic.
    /// </summary>
    public partial class NApplicationManager : NNode
    {
        /// <summary>
        /// Singleton <see cref="NApplicationManager"/> instance.
        /// </summary>
        public static NApplicationManager INSTANCE => _instance ?? throw new RNotReadyException();
        private static NApplicationManager? _instance;

        /// <summary>
        /// Tasks to run immediately before application closes.
        /// </summary>
        public event Func<Task>? OnClosing; // Tasks to run on close.
        /// <summary>
        /// Is the close process ongoing.
        /// </summary>
        public bool Closing { get; private set; } = false; // game is closing



        /// <inheritdoc/>
        public NApplicationManager()
        {
            if (_instance is not null)
                throw new InvalidOperationException("Game manager can only be instantiated once");
            _instance = this;
        } // end ctor

        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            GetTree().AutoAcceptQuit = false; // cancel auto quite to overwrite exit logic.
        } // end _Ready()

        /// <inheritdoc/>
        public override async void _Notification(int what)
        {
            base._Notification(what);
            if (what == NotificationWMCloseRequest
                || what == NotificationPaused)
            {
                if (Closing) // assure only can close once
                    return;
                Closing = true;

                if (OnClosing is not null)
                    foreach (Task t in OnClosing.GetInvocationList().Cast<Func<Task>>().Select(x => x()))
                        await t; // await all on closing tasks
                GetTree().Quit(); // close the application.
            }
        } // end _Notification()
    } // end class
} // end namespace
