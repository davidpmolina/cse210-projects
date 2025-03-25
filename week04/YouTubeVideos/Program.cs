using System;
using System.Collections.Generic;

public class Comment
{
    public string commenterName;
    public string commentText;

    public Comment(string name, string text)
    {
        commenterName = name;
        commentText = text;
    }

    public override string ToString()
    {
        return commenterName + ": " + commentText;
    }
}

public class Video
{
    public string videoTitle;
    public string videoAuthor;
    public int videoLength;
    public List<Comment> videoComments;

    public Video(string title, string author, int length)
    {
        videoTitle = title;
        videoAuthor = author;
        videoLength = length;
        videoComments = new List<Comment>();
    }

    public int howManyComments()
    {
        return videoComments.Count;
    }

    public override string ToString()
    {
        return "Title: " + videoTitle + ", Author: " + videoAuthor + ", Length: " + videoLength + " seconds, Comments: " + howManyComments();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        List<Video> allVideos = new List<Video>();

        Video firstVideo = new Video("Funny Cats", "CatLover", 300);
        firstVideo.videoComments.Add(new Comment("Bob", "LOL!"));
        firstVideo.videoComments.Add(new Comment("Alice", "Cute!"));
        firstVideo.videoComments.Add(new Comment("Charlie", "Meow!"));

        Video secondVideo = new Video("Coding Tutorial", "CodeGuy", 600);
        secondVideo.videoComments.Add(new Comment("Dave", "Thanks!"));
        secondVideo.videoComments.Add(new Comment("Eve", "Helpful!"));
        secondVideo.videoComments.Add(new Comment("Frank", "Nice!"));
        secondVideo.videoComments.Add(new Comment("Grace", "Great tutorial"));

        Video thirdVideo = new Video("Cooking Time", "Chef", 450);
        thirdVideo.videoComments.Add(new Comment("Henry", "Yummy!"));
        thirdVideo.videoComments.Add(new Comment("Ivy", "Looks good!"));
        thirdVideo.videoComments.Add(new Comment("Jack", "I will try it!"));

        Video fourthVideo = new Video("Japan Trip", "Traveler", 900);
        fourthVideo.videoComments.Add(new Comment("Karen", "Wow!"));
        fourthVideo.videoComments.Add(new Comment("Liam", "So cool!"));
        fourthVideo.videoComments.Add(new Comment("Mia", "I want to go!"));

        allVideos.Add(firstVideo);
        allVideos.Add(secondVideo);
        allVideos.Add(thirdVideo);
        allVideos.Add(fourthVideo);

        foreach (Video oneVideo in allVideos)
        {
            Console.WriteLine(oneVideo);

            foreach (Comment oneComment in oneVideo.videoComments)
            {
                Console.WriteLine("  - " + oneComment);
            }

            Console.WriteLine();
        }
    }
}