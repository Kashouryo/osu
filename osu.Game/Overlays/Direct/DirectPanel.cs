// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using OpenTK;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Game.Beatmaps.Drawables;
using osu.Game.Database;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;

namespace osu.Game.Overlays.Direct
{
    public abstract class DirectPanel : Container
    {
        protected readonly BeatmapSetInfo SetInfo;

        protected DirectPanel(BeatmapSetInfo setInfo)
        {
            SetInfo = setInfo;
        }

        protected List<DifficultyIcon> GetDifficultyIcons()
        {
            var icons = new List<DifficultyIcon>();

            foreach (var b in SetInfo.Beatmaps)
                icons.Add(new DifficultyIcon(b));

            return icons;
        }

        protected Drawable GetBackground(TextureStore textures, bool doubleSize)
        {
            return new DelayedLoadWrapper(new BeatmapSetBackgroundSprite(SetInfo, doubleSize)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                FillMode = FillMode.Fill,
                OnLoadComplete = d => d.FadeInFromZero(400, EasingTypes.Out),
            }) { RelativeSizeAxes = Axes.Both, TimeBeforeLoad = 300 };
        }

        public class Statistic : FillFlowContainer
        {
            private readonly SpriteText text;

            private int value;
            public int Value
            {
                get { return value; }
                set
                {
                    this.value = value;
                    text.Text = Value.ToString(@"N0");
                }
            }

            public Statistic(FontAwesome icon, int value = 0)
            {
                Anchor = Anchor.TopRight;
                Origin = Anchor.TopRight;
                AutoSizeAxes = Axes.Both;
                Direction = FillDirection.Horizontal;
                Spacing = new Vector2(5f, 0f);

                Children = new Drawable[]
                {
                    text = new OsuSpriteText
                    {
                        Font = @"Exo2.0-SemiBoldItalic",
                    },
                    new TextAwesome
                    {
                        Icon = icon,
                        Shadow = true,
                        TextSize = 14,
                        Margin = new MarginPadding { Top = 1 },
                    },
                };

                Value = value;
            }
        }

        private class BeatmapSetBackgroundSprite : Sprite
        {
            private readonly BeatmapSetInfo set;
            private readonly bool doubleSize;

            public BeatmapSetBackgroundSprite(BeatmapSetInfo set, bool doubleSize)
            {
                this.set = set;
                this.doubleSize = doubleSize;
            }

            [BackgroundDependencyLoader]
            private void load(TextureStore textures)
            {
                if (set.OnlineInfo?.Covers?.Card != null)
                    Texture = textures.Get(doubleSize ? set.OnlineInfo.Covers.Card2x : set.OnlineInfo.Covers.Card);
            }
        }
    }
}
