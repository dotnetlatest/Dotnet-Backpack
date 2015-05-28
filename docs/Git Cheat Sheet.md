###Git Cheat Sheet

----------

######Create a new repository

- ```Git init```

######Checkout a repository 
- ```Git clone /path/to/repository [local]```
- ```Git clone username@host:/path/to/repository [remote]```

######Workflow
your local repository consists of three "trees" maintained by git. 

-  the first one is your Working Directory which holds the actual files. 
- the second one is the Index which acts as a staging area and finally the HEAD which points to the last commit you've made.

######Add & commit
You can propose changes (add it to the Index) using 

- ```Git add <filename>```
- ```Git add *```

To commit these files use

- ```Git commit - m "Commit message"```

Now the file is commited to the HEAD, but not in your remote repoitory yet

######Pushing changes

Your changes are now in the head of your local working copy. To send those changes to your remote repository , execute

- ```Git push origin master```

Change master to whatever branch you want to push changes to 

If you have not cloned an existing repository and want to connect your repository to a remote server, you need to add it with

- ```git remote add origin <server>```

Now you are able to push your changes to the selected remote server

######Update & merge

To update your local repository to the newest commit execute

- ```Git pull```
- ```Git pull origin [BRANCH]```

Switching to a new branch

- ```Git checkout -b [Branch]```

####HOW-TO

######Force Git to overwrite local files on pull

- ```git fetch --all```
- ```git reset --hard origin/master```

>git fetch downloads the latest from remote without trying to merge or rebase anything.

>Then the git reset resets the master branch to what you just fetched. The --hard option changes all the files in your working tree to match the files in origin/master

###### How to Stash changes

- ``` git stash ```

> `git stash` Saves your working directory and index to a safe place. and 2) Restores your working directory and index to the most recent commit.


###### How to delete a remote branch

- ```git push origin --delete <branchName>```
