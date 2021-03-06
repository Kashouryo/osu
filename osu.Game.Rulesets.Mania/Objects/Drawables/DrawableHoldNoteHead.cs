// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace osu.Game.Rulesets.Mania.Objects.Drawables
{
    /// <summary>
    /// The head of a <see cref="DrawableHoldNote"/>.
    /// </summary>
    public class DrawableHoldNoteHead : DrawableNote
    {
        public DrawableHoldNoteHead(DrawableHoldNote holdNote)
            : base(holdNote.HitObject.Head)
        {
        }

        public void UpdateResult() => base.UpdateResult(true);

        public override bool OnPressed(ManiaAction action) => false; // Handled by the hold note

        public override void OnReleased(ManiaAction action)
        {
        }
    }
}
