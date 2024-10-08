This project simulates a keypad phone, allowing users to navigate between applications and move within lists using specific buttons. The interface is simple yet effective, providing essential functionalities for users to interact with.

General View:

<img width="185" alt="GenelGörünüm" src="https://github.com/user-attachments/assets/ed8d2791-047a-4e37-8988-f49e99fde335">

İnside some of the apps:

<img width="193" alt="RehberApp" src="https://github.com/user-attachments/assets/472d050d-aef2-460e-a484-43272d1f173e">

<img width="190" alt="MusicApp" src="https://github.com/user-attachments/assets/30d6646f-9729-4360-aa97-18065b6c1817">

<img width="181" alt="RehberAppCall" src="https://github.com/user-attachments/assets/cf4d970c-ee20-4bc6-b86a-81801c669256">

<img width="182" alt="RehberAppUncall" src="https://github.com/user-attachments/assets/fb04c2ad-77a1-426c-8785-7165f8cbc423">

A-Project Functionality

1-Navigation with Directional Buttons:

* The defined up, down, left, and right buttons enable users to switch between applications and navigate within ListBox controls when inside an application.

* For instance, when in the contacts application, these buttons allow users to scroll through the contact list.

2-Okey Button:

* The central Okey button functions similarly to the Enter key on a keyboard. This button opens the selected application or confirms the selection within an application.

* For example, if a song is selected in the music application, pressing the Okey button will start playing that song.

3-Date and Time Display:

* The application displays the current date and time, which is updated regularly and shown on the screen.

4-Volume Control:

* The volumeUp, volumeDown, and mute buttons control the volume level.
* The volume is restricted to a maximum (10) and a minimum (0) level. Users cannot increase the volume beyond the maximum or decrease it below the minimum level.

Volume Down:

<img width="187" alt="SesDüzeyiKısıldı" src="https://github.com/user-attachments/assets/f331044c-567f-456f-9424-a2da0ee93c5d">

Mute:

<img width="186" alt="Sessizde" src="https://github.com/user-attachments/assets/4293dbb3-64eb-4f5c-bd53-73800841c5f4">

Volume down when the phone is mute (restriction):

<img width="187" alt="SessizdeKısıldı" src="https://github.com/user-attachments/assets/d55627f4-62df-4d2b-abbd-207ba6076a1d">

Volume up when the phone has max volume (restriction):

<img width="176" alt="SesDüzeyiMax" src="https://github.com/user-attachments/assets/6cefb9b3-7609-466a-8ff2-b143d48c3a99">

5-Home Button for Returning to the Main Screen:

* The home button allows users to return to the main screen when inside an application.
* Pressing this button closes the current application and redirects the user to the main menu.

6-Keypad Phone Simulation:

* This project is designed as a simulation of a keypad phone interface, following the navigation and control logic of older phone models. It provides a simple yet functional system for user interaction.

B-Technical Details

1-Technologies Used:

* The project is developed using the C# programming language and .NET Framework, with Windows Forms as the development platform.

* It is integrated with an SQL Server database for handling data.

2-Database Integration:

* The contacts and music applications are integrated with an SQL database. Contacts in the contacts app and music information in the music app are dynamically fetched from the database and displayed in a ListBox.

* SqlConnection and SqlCommand objects are used for connecting to the database and retrieving data.

  <img width="235" alt="Veritabanı" src="https://github.com/user-attachments/assets/ecfca015-1787-4989-a9ab-ce9069f0a075">

3-Navigation Logic:

* The application tracks which button is currently in focus using the currentButton variable, allowing users to navigate the screen effectively.

* When directional buttons are pressed, the focus shifts, and the corresponding button becomes selected. This behavior is managed by the Navigate method.

4-Interface Structure:

* The interface is built using basic UI components such as ListBox, Button, TextBox, and Label.

* The Okey, up, down, left, and right buttons provide a straightforward way for users to navigate and control the application.

5-Customized Functions:

* Each button within the application is programmed to perform a specific function.

* The Unvisible and visible methods are used to show or hide components based on the screen's state.

* A Timer is used to update the date and time information every second.

Summary

This project is a simulation of an old-fashioned keypad phone. Users can navigate between applications using directional buttons, select items using the Okey button, and interact with apps like contacts or music. The project is developed in C# using Windows Forms and is integrated with an SQL database. Additional features include volume control, date and time display, and the ability to return to the main screen with the home button.
