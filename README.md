# Liquid Publishing Owl

Tracking your updates and fixes can prove challenging during a website build or maintenance task. **Liquid Publishing Owl** is a Sitecore plug-in that provides detailed insight into the specifics of how and where each publishing effort affects your website application.

This plug-in will help save time and effort while helping minimize the effort spent in debugging and problem-solving your website build.

With a Launchpad-based interface, this Sitecore plug-in provides you easy access to the vital data you need for effectively determining the impact of your editing workflow.

**Features**

The Liquid Publishing Owl Plug-in provides the following features:

-   Accessibility from Sitecore Launchpad
-   High-level view of publishing items in a Sitecore solution
-   Detail-oriented view of any item altered as a result of publishing
-   History retention controls to limit amount of data loading in this section
	- 7-day, 14-day, or 30-day history windows

**Requirements**

This plug in will be cross-browser compatible with the following browsers:
-   Internet Explorer 9+
-   Microsoft Edge
-   Chrome
-   Firefox 75+
-   Supports Sitecore version 9.2+ (MVC)


# Installation

Download the latest release package for your Sitecore version and upload it via the Installation Wizard which can be found under the Development Tools option while viewing the Sitecore Desktop. 

## Configuring Publishing Activity

Out of the box, after installation, you're ready to go! To further define activity log retention details navigate to the Content Editor > Settings > Publishing Activity Owl. 


# Contributing

Help keep this project healthy by contributing bug fixes and new features. Below are a list of pull request guidelines and steps to get started developing locally.  

## Install Sitecore.

In order to contribute you must have a Sitecore installation to test with. This way you'll be able to test your code changes.

## Clone the repo.

After pulling down this repo locally and opening the solution be sure to "Restore NuGet Packages." In doing so, the project will be able to use the required Sitecore DLL references properly. Clean and Rebuild. 

## Serialize items from filesystem.

We've included Sitecore items that help manage the module from the Content Editor. In order to introduce these items to your environment you must update your database with serialized items from the filesystem.

**Configure Serialization**
In order to maintain the same data format across developers ensure "YAML" is configured in `\App_Config\Sitecore\CMS.Core\Sitecore.Serialization.config`on `Line 37`:

`<setting name="Serialization.SerializationType" value="YAML" />`

**Get Latest**
Copy and paste the serialized items from `src\Foundation\PublishingActivityOwl\code\App_Data\serialization` and place them in your webroot. Navigate to the Content Editor and click the "Developer" tab in the ribbon.  *If you do not see this then right-click any other tab and enable the Developer option*. Click "Update database."

**Sync updates**
Go to the item or parent item (tree) and click "Update item," or "Update tree," respectively. This will move items to your `serialization` folder. Drag these items into the repo to be committed. 

We chose to manage items in this way to prevent the use of additional third-party plugins or modules.