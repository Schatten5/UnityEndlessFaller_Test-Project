# Project Review

## Applicant name
Arne Sudholz
---

<!-- Your review goes here -->
<!-- Explain why you did the things that way or any snippet that is word mentioning -->
<!-- If you had any issue and how you resolved them -->

My initial subtask grouping was awkward at best. I should have separated the subtasks in a more suitable way.

The parts that ended up taking longer than expected were randomized platform generation and the score & highscore system.
For randomized platform generation, I wasted time fumbling around with positioning the align parent objects, which Unity seemed to refuse to let me do. Then I realized the ToolHandlePosition was set to Center as opposed to Pivot.

I utilized the Singleton pattern multiple times through the project to ensure that the important manager/controller systems can only exceed as a single instance.

I kept cohesion as high as possible and coupling as low as possible.
I utilized delegates at a few points to keep the number of set references to a minimum. This initially caused me some issues in a case where one started the game, went back to the main menu and then started the game a second time, as I had forgotten to unsubscribe from the events (static references are kept even when the scene is unloaded).

##Initial subtask time estimations
Actual time spent on each task is in brackets

###Basic player movement and control - 15 minutes (15 mins)
This was essentially done already. I had to freeze the rotation of the rigidbody and later apply a physics material so the player couldn't "stick" to platforms/walls.

###Randomized Platform generation - 1.5 hours (2 hours)
This took a little longer due to problems outlined above.

###Score and persistent highscore system - 2 hours (2.5 hours)

###Increasing platform speed and initial platform settings - 1 hour (1.5 hours)
This task was slightly more lengthy than initially expected.

###Pause and Game Over menus - 1 hour (1 hour)

###Bonus parts (Pretty details, highscore visuals, smooth transitions) - 1-2 hours
A lot of the code-related bonus parts I implemented right from the get-go (no scene reload for restart, no PlayerPrefs usage etc). In the end, I didn't have any time to make the game look prettier.