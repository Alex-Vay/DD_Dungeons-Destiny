﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD_Dungeons_Destiny;

public class Button : Component
{
    private MouseState currentMouse;
    private SpriteFont font;
    private bool isHovering;
    private MouseState previusMouse;
    private Texture2D texture;

    public event EventHandler Click;
    public bool Clicked { get; private set; }
    public Color PenColor { get; set; }
    public Vector2 Position { get; set; }
    public Rectangle Rectangle
    {
        get
        {
            return new Rectangle((int)Position.X - texture.Width/2, (int)Position.Y - texture.Height/2, texture.Width, texture.Height);
        }
    }
    public string Text { get; set; }

    //public Button(Color color, SpriteFont font)
    //{
    //    this.font = font;
    //    PenColor = color;
    //}

    public Button (Texture2D texture, SpriteFont font)
    {
        this.texture = texture;
        this.font = font;
        PenColor = Color.Black;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        var color = Color.White;
        if (isHovering)
            color = Color.Gray;

        spriteBatch.Draw(texture, Rectangle, color);

        if (!string.IsNullOrEmpty(Text))
        {
            var x = (Rectangle.X + (Rectangle.Width / 2)) - (font.MeasureString(Text).X / 2);
            var y = (Rectangle.Y + (Rectangle.Height / 2)) - (font.MeasureString(Text).Y / 2);
            spriteBatch.DrawString(font, Text, new Vector2(x, y), PenColor);
        }
    }

    public override void Update(GameTime gameTime)
    {
        previusMouse = currentMouse;
        currentMouse = Mouse.GetState();
        var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);
        isHovering = false;
        if (mouseRectangle.Intersects(Rectangle))
        {
            isHovering = true;
            if (currentMouse.LeftButton == ButtonState.Released && previusMouse.LeftButton == ButtonState.Pressed)
            {
                Click?.Invoke(this, new EventArgs());
            }    
        }
    }
}
