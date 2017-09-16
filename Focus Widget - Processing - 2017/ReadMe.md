# Focus Widget
![alt text](http://www.sunwangshu.com/wp-content/uploads/2017/08/Screen-Shot-2017-08-29-at-8.05.54-PM-996x630.png)

## Intro:
This is a scalable widget I wrote for visualizing my EEG algorithm, and allowing end users to change threshold and get access to other functionalities.

Project Link: http://www.sunwangshu.com/portfolio/focus-widget/

## Classes:
**class W_Focus extends Widget**: The Focus Widget class, contains the visual display and various functions.

**public abstract class BasicSlider**: Abstract basic slider class.

**public class FocusSlider extends BasicSlider**: Movable slider that can set a value between valMin and valMax.

**public class FocusSlider_Static extends BasicSlider**: A slider that stays in place and can set a value between valMin and valMax.
