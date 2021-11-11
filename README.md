# Creating Cross-Platform C# Applications with Uno Platform

<a href="https://www.packtpub.com/product/creating-cross-platform-c-applications-with-uno/9781801078498"><img src="https://static.packt-cdn.com/products/9781801078498/cover/smaller" alt="Creating Cross-Platform C# Applications with Uno Platform" height="256px" align="right"></a>

This is the code repository for [Creating Cross-Platform C# Applications with Uno Platform](https://www.packtpub.com/product/creating-cross-platform-c-applications-with-uno/9781801078498), published by Packt.

**Build apps with C# and XAML that run on Windows, macOS, iOS, Android, and WebAssembly**

## What is this book about?
Developers are increasingly being asked to build native applications that run on multiple operating systems and in the browser. In the past, this would have meant learning new technologies and making multiple copies of an application. But the Uno Platform allows you to use tools, languages, and APIs you already know from building Windows apps to develop apps that can also run on other platforms. This book will help you to create customer-facing as well as line-of-business apps that can be used on the device, browser, or operating system of your choice.

This book covers the following exciting features: 
* Understand how and why Uno could be the right fit for your needs
* Set up your development environment for cross-platform app development with the Uno Platform and create your first Uno Platform app
* Find out how to create apps for different business scenarios
* Discover how to combine technologies and controls to accelerate development
* Go beyond the basics and create 'world-ready' applicationsers

If you feel this book is for you, get your [copy](https://www.amazon.com/dp/1801078491) today!

<a href="https://www.packtpub.com/?utm_source=github&utm_medium=banner&utm_campaign=GitHubBanner"><img src="https://raw.githubusercontent.com/PacktPublishing/GitHub/master/GitHub.png" 
alt="https://www.packtpub.com/" border="5" /></a>


## Instructions and Navigations
All of the code is organized into folders. For example, Chapter02.

The code will look like the following:
```
namespace HelloWorld.Helpers
{
    public class Greetings
    {
        public static string GetStandardGreeting()
        {
            return "Hello from a cross platform library!";
        }
    }
}
```

**Following is what you need for this book:**
This book is for developers who are familiar with app development for Windows and want to use their existing skills to build cross-platform apps. Basic knowledge of C# and XAML is required to get started with this book. Anyone with basic experience in app development using WPF, UWP, or WinUI will be able to learn how to create cross-platform applications with the Uno Platform.

With the following software and hardware list you can run all code files present in the book (Chapter 1-8).

### Software and Hardware List

| Chapter  | Software required                   | OS required                        |
| -------- | ------------------------------------| -----------------------------------|
| 1-8        | Uno Platform 3.9                     | Windows 10 or macOS 11 (Big Sur)  |



We also provide a PDF file that has color images of the screenshots/diagrams used in this book. [Click here to download it](https://static.packt-cdn.com/downloads/9781801078498_ColorImages.pdf).

## Code in Action

Click on the following link to see the Code in Action:

[Youtube](https://bit.ly/3yHTfYL)

### Related products <Other books you may enjoy>
* C# 9 and .NET 5 – Modern Cross-Platform Development- Fifth Edition [[Packt]](https://www.packtpub.com/product/c-9-and-net-5-modern-cross-platform-development-fifth-edition/9781800568105) [[Amazon]](https://www.amazon.com/dp/180056810X)

* Mobile Development with .NET - Second Edition [[Packt]](https://www.packtpub.com/product/mobile-development-with-asp-net-core-5-second-edition/9781800204690) [[Amazon]](https://www.amazon.com/dp/1800204698)

## Get to Know the Authors
**Matt Lacey**
has been building desktop and mobile software since the '90s. He currently works as an independent development consultant and focuses on helping developers to create better software. Having worked in companies of all sizes and in a wide variety of industries, he brings this breadth of experience to present a viewpoint that considers technology, business, and design.
Matt is a Microsoft MVP in Windows Development, regularly speaks at user groups and conferences in multiple countries, and is a prolific contributor to a plethora of open-source projects. He lives in the UK with his wife and two children.

**Marcel Wagner**
is a full-stack software developer and open source contributor. He is a Microsoft MVP in Windows Development, and a top contributor to the WinUI library and the XAML Controls Gallery while also contributing to other projects and libraries including the Windows Community Toolkit and Uno Platform.
Marcel graduated with a Bachelor of Science in Computer Science and has been developing applications and services with a wide variety of technologies including React, Java, C#, C++, UWP and Uno Platform. He currently resides in Germany.




## Errata

Page 33: For the Android Phone to show up in the list, the developer mode needs to be enabled which is missing in the book. Instructions for this can be found [here](https://docs.microsoft.com/en-us/xamarin/android/get-started/installation/set-up-device-for-development)

Page 42: The code `#elif HAS_UNO_WASM` needs to be changed to `#elif HAS_UNO_WASM || __WASM__` 