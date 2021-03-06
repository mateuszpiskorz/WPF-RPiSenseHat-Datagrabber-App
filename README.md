# WPF-RPiSenseHat-Datagrabber-App
Windows desktop application designed to work with RaspberryPI and SenseHat. University Project.
## Introduction
This is a simple WPF application providing functionality to operate SenseHat. The program exhibits data collected from SenseHat's sensors in real-time graphs provided by OXYplot package.
Moreover, the application allows to control SenseHat's LED matrix by selecting single LED and customizing its color. User is able to adjust core server parameters eg. IP Address as well as data parameters eg. Sample time or Maximum samples stored in graph.
### Screenshots
<img src="/images/ConfigView.png"  alt="Configuration Page" width="50%" height="50%">
<img src="/images/LedView.png"  alt="Led Page" width="50%" height="50%">
<img src="/images/graphView.png"  alt="Graph Page" width="50%" height="50%">

### Attention!
**Application requires server-side API and Python scripts (SenseHat functionality) on RaspberryPi which are not included in this repository.**
## Technologies/Patterns
Windows Presentation Foundation(WPF), OXYplot, MVVM pattern, C#, XAML
## TODOs
* Improve apperence of  GUI
* Rewrite for cleaner code
* Adaptation to the IoC principle
