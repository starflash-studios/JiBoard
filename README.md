# JiBoard
*The following information is up-to-date as of version 0.2.2.1*

 JiBoard is a simple, portable Japanese IME built within .NET Framework 4.8.
 
----------

# General Usage
![https://user-images.githubusercontent.com/16698604/68859162-a7473b80-0721-11ea-9c94-d9586109ed5d.png](https://user-images.githubusercontent.com/16698604/68859162-a7473b80-0721-11ea-9c94-d9586109ed5d.png)

There are various aspects to JiBoard, but each one can easily be broken down small portions.

## Display Language
![https://user-images.githubusercontent.com/16698604/68859150-a6160e80-0721-11ea-87f2-2d48b22bdd40.png](https://user-images.githubusercontent.com/16698604/68859150-a6160e80-0721-11ea-87f2-2d48b22bdd40.png)

Due to the multi-lingual nature of this program, various languages are supported within the UI out-of-the-box. As of version 0.2.2.1, this includes both Japanese and English.
When the program starts, it will check Windows for the current Input Language, and set the display language of the program accordingly (ie if the user was using the inbuilt Japanese IME, the program would display in Japanese).
This may also be changed manually by clicking on the dropdown and changing it to the desired language.

## Hiragana
![https://user-images.githubusercontent.com/16698604/68859155-a6aea500-0721-11ea-8b9d-9cbc09b54e23.png](https://user-images.githubusercontent.com/16698604/68859155-a6aea500-0721-11ea-8b9d-9cbc09b54e23.png)

Seen above is the main portion of JiBoard - the Kana Keyboard. Within the 'Hiragana' language, a user simply clicks on the desired character to copy it to their clipboard; or append it to the textbox if within string-mode. *See 'String-Mode' for more information.*

### Kana Modes
![https://user-images.githubusercontent.com/16698604/68859153-a6160e80-0721-11ea-8773-ab202f55dd63.png](https://user-images.githubusercontent.com/16698604/68859153-a6160e80-0721-11ea-8773-ab202f55dd63.png)

To the right of the Kana Keyboard is the Kana Switcher. The user may click on the dropdown to swap between various Kana modes. In relation to the Hiragana keyboard, these options are: Normal, Small, Handakuten and Dakuten. Changing this will result in the Kana Keyboard itself changing to reflect the desired Kana Mode.

### Pronunciation
![https://user-images.githubusercontent.com/16698604/68859157-a6aea500-0721-11ea-95c7-8801831f9160.png](https://user-images.githubusercontent.com/16698604/68859157-a6aea500-0721-11ea-95c7-8801831f9160.png)

Below the Kana Switcher is a checkbox to enable/disable the pronunciation view. Changing this will result in the Kana Keyboard itself changing to show the pronunciation of the various Kana if enabled.

## Punctuation
![https://user-images.githubusercontent.com/16698604/68859158-a7473b80-0721-11ea-85e9-af95160df6c0.png](https://user-images.githubusercontent.com/16698604/68859158-a7473b80-0721-11ea-85e9-af95160df6c0.png)

Below the Kana Keyboard is the Punctuation Keyboard. This behaves much in the same way as the Kana Keyboard, though utilising different punctuationally-based characters. These too may be appended to the textbox if within string-mode. *See 'String-Mode' for more information.*

## String Mode
![https://user-images.githubusercontent.com/16698604/68859161-a7473b80-0721-11ea-8836-dd3d0149efb1.png](https://user-images.githubusercontent.com/16698604/68859161-a7473b80-0721-11ea-8836-dd3d0149efb1.png)

Below the Punctuation Keyboard is the region dedicated to 'String Mode'. When a user clicks on a character, by default it is copied to the system buffer (generally referred to as the 'Clipboard', and may be pasted elsewhere.
If the checkbox to the far bottom-left is enabled, the program enters 'String Mode'. This means that when a user clicks on a character, it is instead appended to the textbox in the center. This allows the user to type out a full sentence and copy the whole thing when complete, instead of copying and pasting character-by-character.
The user may then click the 'Copy' button on the far right to copy the text, or the 'X' button to the left of it to clear the textbox and start again.