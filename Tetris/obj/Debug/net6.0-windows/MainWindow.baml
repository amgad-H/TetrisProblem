   M S B A M L   `   `   `  ����A  =Tetris, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:TetrisTetris  5   	       Tetris.MainWindow   5   	   W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Tetris     5   	   -   5          � Title$  Tetris��5   	   $	��600��6   $	��800��6%   $	��600��5	   	   $	��600��6   
�������$
��Sans)�5   	   $��30(�6   .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��auto �6   �� 5      $��* �6   ��5   
   �� 5       �� ImageSource$ Assets/Background.png��6   .+�5   
   (� $��1��6   $��1��5      $��0,0,0,20q�5      .��5       �� -   5         ��Name$ 
GameCanvas��5       ���#101010$	��250��5"      $	��510��5#      $
}�True��5$      5%      5'      �� 5)   
   -   5         ��Name$ 	ScoreText��5)      $��0��5*      $��1��5+      $��Score: ��5,      $
��0,10q�5-       �� TextAlignment$ Center=�5.      �� 50   
   $��1��6   $��Center=�51      $��Center=�52      $	��235��62   .+�53      �� $��
HighScores��6   $ Center=�54      �� 56         Players#���   6   $��Stretch=�57      
���   �$��5,5,5,3q�59      $��10(�5:      $
}�True��5;       �� View 5?      �� 5@      .P�5A      �� $
��Name��6)    �� DisplayMemberBinding 5B      �� *Key+�� 5C      $��Score��6)    5D      �� *Value.Score+�� 5E      $
��Date��6)    5F      �� *
Value.Date+5G      5I      5J      �� 5L   
   $��1��6   $��2��5M      $��Center=�5N      $
��Left=�5O      .+�5P      �� $
��Next��6   $ Center=�5Q      �� 5R      -   5        	 ��Name$	 	NextImage��5R      $��20q�5S      $	��125��5T      5U      � 5W   
   -   5        
 �Name$
 GameOverMenu��5W      ���	#CC000000$��2��5Y      $��3��5Z      $y�Hidden=�5[      .+�5\      �� $��Center=�6   $��Center=�5]      .+�5^      �� $��	Game Over��6   $��50(�5_      $ Center=�5`      �� 5b      -   5        $ FinalScoreText��5b      $��Score: ��5c      $��38(�5d      $ Center=�5e      �� 5g      -   5        $��
Play Again��5g      
�������$��0,20,0,0q�5i      $��5q�5j      5k      �� 5m      -   5        $��Return to Menu��5m      
�������$��0,20,0,0q�5o      $��5q�5p      5q      5r      5s      � 5u   
   -	   5        $
 GameStartMenu��5u      $��2��5v      $��3��5w      $y�Visible=�5x      ��5y      �� 5z      $) #/Assets/Game-Tetris-Backgrounds.jpg��6    j� Stretch$
 Fill=�6O   .+�5|      �� $��Center=�6   $��Center=�5}      .+�5~      �� -
   5        $	 Tetris_Logo��5~      $��Assets/Tetris_Logo.png��6-   $	��304��6M   $��UniformToFill=�6Y   $	��236��6q   �� 5�      -   5        $
��Play��5�      
������$��0,20,0,0q�5�      $��5q�5�      5�      �� 5�      -   5         ��Name$ PlayerNameTextBox��5�      $	��306��65   $��44��6A   #��� ��6M   $��Enter Player Name��6�   $��20(�6�   $��Center=�6�   #��� ��6�   6L  5�      5�      � 5�   
   -   5        $
 GamePauseMenu��5�      ���	#CC000000$��2��5�      $��3��5�      $y�Hidden=�5�      .+�5�      �� $��Center=�6   $��Center=�5�      .+�5�      �� $��Game Paused��6   $��50(�5�      $ Center=�5�      �� 5�      -   5        $ CurrentScoreText��5�      $��Score: ��5�      $��38(�5�      $ Center=�5�      �� 5�      -   5        $��Continue��5�      
����ح�$��0,20,0,0q�5�      $��5q�5�      5�      �� 5�      -   5        $��Return to Menu��5�      
�������$��0,20,0,0q�5�      $��5q�5�      5�      5�      5�      5�      5�      