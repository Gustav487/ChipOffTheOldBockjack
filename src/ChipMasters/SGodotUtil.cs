using Godot;
using System;
using System.Text.RegularExpressions;

namespace ChipMasters
{
    /// <summary>
    /// Extension and utility methods for working with Godot things.
    /// </summary>
    public static class SGodotUtil
    {
        private static readonly Regex SCENE_PATH_REGEX = new(@"res://[A-Za-z_]*(/[A-Za-z_]*)*\.tscn");



        /// <summary>
        /// Validate the <paramref name="scenePath"/>, and create an action to change to that scene.
        /// </summary>
        /// <param name="scenePath"></param>
        /// <param name="sceneTree"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public static Action SceneChangeToFile(string? scenePath, SceneTree sceneTree)
        {
            if (!SCENE_PATH_REGEX.IsMatch(scenePath.AssertNotNull()))
                throw new ArgumentException($"Path: {scenePath}, wasn't considered valid.");
            if (!ResourceLoader.Exists(scenePath))
                throw new ArgumentException($"File: {scenePath}, was not found.");

            return () =>
            {
                Error e = sceneTree.ChangeSceneToPacked(ResourceLoader.Load<PackedScene>(scenePath));
                if (e != Error.Ok)
                    throw new Exception($"Failed to change to scene with error: {e}");
            };
        } // end SceneChangeToFile()

        /// <summary>
        /// Convert <see cref="Godot.Vector2"/> to <see cref="System.Numerics.Vector2"/>.
        /// </summary>
        /// <param name="vec2"></param>
        /// <returns></returns>
        public static System.Numerics.Vector2 To(this Vector2 vec2) => new(vec2.X, vec2.Y);

    } // end class
} // end namespace
