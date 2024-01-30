# whatsappStickerMaker
## Purpose
Similar to applications on android, such as "sticker maker", this desktop GUI allows users to create whatsapp sticker packs and export them to the .wasticker format.

## Technologies used
The framework used is WPF C#, which includes xaml.

## Program functionaility
The program allows the user to do the following:
- Fill in author name
- Fill in pack name
- Select image for the pack icon(96x96 pixels)
- Select images for the stickers(512x512 pixels)
- Clear the data
- Clear the grid
- Export to .wasticker format
- Automatic reordering of the stickers

The program works in a simple loop:
1. User clicks on the "Create" button
2. A 3x10 grid appears for the user's stickers, and a slot for the Author's name, Pack icon, and title of the pack.
3. Once the user has filled the grid with their desired amount of stickers, they have three options:
    <br>i)The "Clear" button hides all input compotents
    <br>ii)The "Clear data" button clears all input fields
    <br>iii)The "Export" button validates the data, then collects all the data required for the sticker pack, compresses it into a .zip, and renames it into the required
        .wasticker format
4. If the option "Reorder after selection" is ticked in the options menu, once a sticker is added, it will automatically reorder the sticker.
i.e) If the slots 1,2,3 are used, and the user decides to put a sticker at slot 6, the app will automatically move the sticker to slot 4

## Visuals

## Highlights of the program
- Dynamically stores information about the stickers. Meaning, that if a user wants to put 3 stickers only, regardless of which slots they use, the outputting sticker pack will be the same.
i.e) If they put sticker 1,2,and 3 into slots 1,4,5. The output will be the same as if instead they put it into slot 1,2,3 rather
- The program uses custom controls for the various components. Additionally there is an event listener.
- The dependency inversion principle has been utilized.

## What could be done better
- A "drag and drop" feature could be implemented to allow user's to reorder their stickers
- Allow users to rescale the application to suit their needs. At the moment, the application is given a fixed width and size
- A better visual design
