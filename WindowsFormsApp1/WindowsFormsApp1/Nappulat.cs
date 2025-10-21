using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1;

public class Nappulat
{
    public string Piece {  get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    Dictionary<string, int> pieceLocations;


    public Nappulat()
    {
        
    }


    public static ChessPiece King()
    {
        return new ChessPiece("♚", Color.Gray, 7, 3);
    }

    public static ChessPiece Queen()
    {
        return new ChessPiece("♛", Color.Gray, 7, 4);
    }

    public static ChessPiece Bishop()
    {
        return new ChessPiece("♝", Color.Gray, 7, 2);
    }

    public static ChessPiece Knight()
    {
        return new ChessPiece("♞", Color.Gray, 7, 1);
    }

    public static ChessPiece Rook()
    {
        return new ChessPiece("♜", Color.Gray, 7, 0);
    }

    public static ChessPiece Pawn()
    {
        return new ChessPiece("♟", Color.Gray, 6, 0);
    }

    public void Label_Click(object sender, EventArgs e)
    {
        Label clickedLabel = sender as Label;
        if (clickedLabel != null)
        {
            if (pieceLocations.ContainsValue(X))
            {
                Point clickedPieceLocation = (Point)clickedLabel.Tag;
                MessageBox.Show("Toimii");
            }
        }
    }

    public void ok()
    {
        Dictionary<string, int> pieceLocations = new Dictionary<string, int>();

        pieceLocations.Add("King", 4);



    }

    public override string ToString()
    {
        return $"{Piece} @ ({X}, {Y})";
    }
}