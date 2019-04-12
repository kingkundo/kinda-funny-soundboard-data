# Kinda Funny Soundboard app content

Are you part of the KF community? Contribute audio clips and images to the Kinda Funny Soundboard app!

I created this app years ago but life has taken hold and I no longer have time to maintain it. Instead of letting it just die, I've decided it's best to open it up to the community and allow you guys to contribute.

There are new members of KF since the last update, so please consider adding more clips and 'characters' to the app 😊. Soon I will create a credits section, allowing you to get credit for your work. This hand over of the project to the community is a work in progress though and please bear with me while we get this off the ground.

## How to contribute

It's simple. Fork this repo, add new content, and send a pull request. I will merge the changes and add your content to the app. Simple as that.

Adding your content is simple, too. The full steps are laid out for you below.

### Adding images

You can add new images of your favourite KF members to help give the app more visual variety.

* Make sure your image is exactly 200x200 resolution, in .png format.

* Make sure your image is all lowercase with no spaces, to keep with the standard.

* Copy the image into the 'photos' folder.

* Open up the 'clips.json' file, and rename the 'imageTitle' field of all the clips that you want to use your new image file, to the name of your new image file.

And that's it. If your image is the right size, all in lowercase, with no spaces in the title, and is set to appear on audioclips... Then all is ready to go!

### Adding clips

You can add new audio clips of funny KF moments just as easily as you add images... In fact it's mostly the same steps.

* Make sure your audio clip is an .mp3, relatively short, with a small file size. Unreasonably large clips will be rejected as we don't want to bog down app user's memory.

* Copy the new audio clip to the clips folder, ensuring that the file name is all lowercase and without spaces.

* Open up the 'clips.json' file, and add a new entry for your new audioclip, referencing an image from the 'images' folder for the 'imageTitle'

After saving... Your job is done! Your clip will be added correctly.

### Adding tabs

Tabs, or new members, are the easiest of all to add.

* Open up the 'tabs.json' file.

* Add a new entry for the new tab that you want to create. If a member of Kinda Funny doesn't appear in the app, but you have clips for them, add them here!

* Follow the above processes for adding clips and images to populate this new tab with your content.

## Making sure it works

Once you've finished adding your content, go to the 'tools' folder and run the validator to ensure that your content is formatted correctly. If your changes pass validation, make a pull request and I will add all your wonderful content to the app.

## The future of this community project

I want to be able to step away from this project in the future, leaving EVERYTHING in the hands of you guys in the Kinda Funny community. This is the start of my work towards this goal.

The next stages of development will look like this:

* Find a way to get you guys to be able to easily test your changes without having to leave that to me. I'm working on it!

* See how the community responds to these changes, and how willing people are to contribute.

* Continue working with the community, trying to find members willing to take this project over fully who I trust to ensure quality remains high.

## Thanks

Thanks for supporting this project over the years. I have spent hours cutting out clips from Kinda Funny's library of content to give back to you guys in the form of this app, and I hope that together we can make it even better.

## Final word

The license for everything in this repo is MIT. I have released it all to you guys to do whatever you want to. If you want to just grab the audio clips you're welcome to do that.