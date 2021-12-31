﻿using Bunit;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using WebUI.Client.Services;
using WebUI.Client.Test.Extensions;
using WebUI.Shared.Instructors.Queries.GetInstructorsOverview;
using Xunit;

namespace WebUI.Client.Test.Pages.Instructors
{
    public class InstructorsTests : BunitTestBase
    {
        [Fact]
        public async Task Instructors_ClickCreateButton_OpensDialog()
        {
            var fakeInstructorService = A.Fake<IInstructorService>();
            Context.Services.AddScoped(x => fakeInstructorService);

            var fakeCourseService = A.Fake<ICourseService>();
            Context.Services.AddScoped(x => fakeCourseService);

            var fakeStudentService = A.Fake<IStudentService>();
            Context.Services.AddScoped(x => fakeStudentService);

            var fakeUploadService = A.Fake<IFileuploadService>();
            Context.Services.AddScoped(x => fakeUploadService);

            var dialog = Context.RenderComponent<MudDialogProvider>();
            Assert.Empty(dialog.Markup.Trim());

            var comp = Context.RenderComponent<Client.Pages.Instructors.Instructors>();
            Assert.NotEmpty(comp.Markup.Trim());

            comp.Find("h2").TrimmedText().Should().Be("Instructors");
            comp.Find("#CreateButton").Should().NotBeNull();

            comp.Find("#CreateButton").Click();

            Assert.NotEmpty(dialog.Markup.Trim());
        }

        [Fact]
        public void Instructors_ClickSearch_CallsInstructorService()
        {
            var fakeInstructorService = A.Fake<IInstructorService>();
            Context.Services.AddScoped(x => fakeInstructorService);

            var fakeCourseService = A.Fake<ICourseService>();
            Context.Services.AddScoped(x => fakeCourseService);

            var fakeStudentService = A.Fake<IStudentService>();
            Context.Services.AddScoped(x => fakeStudentService);

            var comp = Context.RenderComponent<Client.Pages.Instructors.Instructors>();
            Assert.NotEmpty(comp.Markup.Trim());

            comp.Find("#SearchButton").Should().NotBeNull();
            comp.Find("#SearchButton").Click();

            A.CallTo(() => fakeInstructorService.GetAllAsync(A<string>.Ignored, A<int?>.Ignored, A<string>.Ignored, A<int?>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public void Instructors_ClickBackToFullList_CallsInstructorService()
        {
            var fakeInstructorService = A.Fake<IInstructorService>();
            Context.Services.AddScoped(x => fakeInstructorService);

            var fakeCourseService = A.Fake<ICourseService>();
            Context.Services.AddScoped(x => fakeCourseService);

            var fakeStudentService = A.Fake<IStudentService>();
            Context.Services.AddScoped(x => fakeStudentService);

            var comp = Context.RenderComponent<Client.Pages.Instructors.Instructors>();
            Assert.NotEmpty(comp.Markup.Trim());

            comp.Find("#SearchButton").Should().NotBeNull();
            comp.Find("#SearchButton").Click();

            A.CallTo(() => fakeInstructorService.GetAllAsync(A<string>.Ignored, A<int?>.Ignored, A<string>.Ignored, A<int?>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public void Instructor_ClickDetailsButton_OpensDialog()
        {
            var instructorsOverviewVM = new InstructorsOverviewVM
            {
                Instructors =
                {
                    new InstructorVM
                    {
                        InstructorID = 1,
                        FirstName = "Instructor",
                        LastName = "X"
                    }
                }
            };

            var fakeInstructorService = A.Fake<IInstructorService>();
            A.CallTo(() => fakeInstructorService.GetAllAsync(A<string>.Ignored, A<int?>.Ignored, A<string>.Ignored, A<int?>.Ignored)).Returns(instructorsOverviewVM);
            Context.Services.AddScoped(x => fakeInstructorService);

            var fakeCourseService = A.Fake<ICourseService>();
            Context.Services.AddScoped(x => fakeCourseService);

            var fakeStudentService = A.Fake<IStudentService>();
            Context.Services.AddScoped(x => fakeStudentService);

            var dialog = Context.RenderComponent<MudDialogProvider>();
            Assert.Empty(dialog.Markup.Trim());

            var comp = Context.RenderComponent<Client.Pages.Instructors.Instructors>();
            Assert.NotEmpty(comp.Markup.Trim());

            comp.FindAll(".OpenInstructorDetailsButton")[0].Should().NotBeNull();
            comp.FindAll(".OpenInstructorDetailsButton")[0].Click();

            Assert.NotEmpty(dialog.Markup.Trim());
        }

        [Fact]
        public void Instructor_ClickEditButton_OpensDialog()
        {
            var instructorsOverviewVM = new InstructorsOverviewVM
            {
                Instructors =
                {
                    new InstructorVM
                    {
                        InstructorID = 1,
                        FirstName = "Instructor",
                        LastName = "X"
                    }
                }
            };

            var fakeInstructorService = A.Fake<IInstructorService>();
            A.CallTo(() => fakeInstructorService.GetAllAsync(A<string>.Ignored, A<int?>.Ignored, A<string>.Ignored, A<int?>.Ignored)).Returns(instructorsOverviewVM);
            Context.Services.AddScoped(x => fakeInstructorService);

            var fakeCourseService = A.Fake<ICourseService>();
            Context.Services.AddScoped(x => fakeCourseService);

            var fakeStudentService = A.Fake<IStudentService>();
            Context.Services.AddScoped(x => fakeStudentService);

            var fakeUploadService = A.Fake<IFileuploadService>();
            Context.Services.AddScoped(x => fakeUploadService);

            var dialog = Context.RenderComponent<MudDialogProvider>();
            Assert.Empty(dialog.Markup.Trim());

            var comp = Context.RenderComponent<Client.Pages.Instructors.Instructors>();
            Assert.NotEmpty(comp.Markup.Trim());

            comp.FindAll(".OpenInstructorEditButton")[0].Should().NotBeNull();
            comp.FindAll(".OpenInstructorEditButton")[0].Click();

            Assert.NotEmpty(dialog.Markup.Trim());
        }
    }
}
