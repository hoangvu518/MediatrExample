CREATE TABLE [dbo].[StudentCourse] (
    [Id]        BIGINT IDENTITY (1, 1) NOT NULL,
    [StudentId] INT    NOT NULL,
    [CourseId]  INT    NOT NULL,
    CONSTRAINT [PK_StudentCourse] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StudentCourse_Course] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Course] ([Id]),
    CONSTRAINT [FK_StudentCourse_Student] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Student] ([Id])
);



