using System;
using XfmaShellTemplate.ViewModels;
using Xunit;
using Moq;
using XamarinFormsMvvmAdaptor;
using XfmaShellTemplate.Services;
using Xamarin.Forms;
using XfmaShellTemplate.Models;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace XfmaShellTemplate.Tests.ViewModels
{
    public class ItemsViewModelTests
    {
        readonly Mock<IDataStore<Item>> mockDataStore;
        readonly Item item;

        public ItemsViewModelTests()
        {
            item = new Item { Id = "1", Text = "Item1", Description = "This is Item1" };

            mockDataStore = new Mock<IDataStore<Item>>();
            mockDataStore.Setup(o => o.GetItemsAsync(true))
                .ReturnsAsync(new List<Item>{item});
        }

        [Fact]
        public void LoadItemsCommand_Constructed_IsNotNull()
        {
            var vm = new ItemsViewModel(
                new Mock<INavigationService>().Object,
                new Mock<IDataStore<Item>>().Object,
                new Mock<IMessagingCenter>().Object);
            Assert.NotNull(vm.LoadItemsCommand);
        }

        [Fact]
        public void LoadItemsCommand_Executed_ItemsPropertyIsNotEmpty()
        {
            var vm = new ItemsViewModel(
                new Mock<INavigationService>().Object,
                mockDataStore.Object,
                new Mock<IMessagingCenter>().Object);

            vm.LoadItemsCommand.Execute(null);
            Assert.Single(vm.Items);
        }

        [Fact]
        public void LoadItemsCommand_ExecutedWhenIsBusy_ItemsPropertyIsEmpty()
        {
            var vm = new ItemsViewModel(
                new Mock<INavigationService>().Object,
                mockDataStore.Object,
                new Mock<IMessagingCenter>().Object);
            vm.IsBusy = true;
            vm.LoadItemsCommand.Execute(null);
            Assert.Empty(vm.Items);
        }

        [Fact]
        public void AddItemCommand_Constructed_IsNotNull()
        {
            var vm = new ItemsViewModel(
                new Mock<INavigationService>().Object,
                new Mock<IDataStore<Item>>().Object,
                new Mock<IMessagingCenter>().Object);
            Assert.NotNull(vm.AddItemCommand);
        }

        [Fact]
        public void AddItemCommand_Executed_NavigatesToNewItemViewModel()
        {
            var mockNavigation = new Mock<INavigationService>();
            mockNavigation.Setup(
                o => o.PushAsync<NewItemViewModel>(null, true)).Verifiable();

            var vm = new ItemsViewModel(
                mockNavigation.Object,
                new Mock<IDataStore<Item>>().Object,
                new Mock<IMessagingCenter>().Object);

            vm.AddItemCommand.Execute(null);

            Mock.Verify(new Mock[]{ mockNavigation});
        }

        [Fact]
        public void ItemSelectedCommand_Constructed_IsNotNull()
        {
            var vm = new ItemsViewModel(
                new Mock<INavigationService>().Object,
                new Mock<IDataStore<Item>>().Object,
                new Mock<IMessagingCenter>().Object);
            Assert.NotNull(vm.ItemSelectedCommand);
        }

        [Fact]
        public void ItemSelectedCommand_Executed_NavigatesToItemDetailViewModel()
        {
            var mockNavigation = new Mock<INavigationService>();
            mockNavigation.Setup(
                o => o.PushAsync<ItemDetailViewModel>(item, true)).Verifiable();

            var vm = new ItemsViewModel(
                mockNavigation.Object,
                new Mock<IDataStore<Item>>().Object,
                new Mock<IMessagingCenter>().Object);

            vm.ItemSelectedCommand.Execute(item);

            Mock.Verify(new Mock[] { mockNavigation });
        }

        [Fact]
        public void ItemsProperty_Constructed_IsEmpty()
        {
            var vm = new ItemsViewModel(
                new Mock<INavigationService>().Object,
                new Mock<IDataStore<Item>>().Object,
                new Mock<IMessagingCenter>().Object);
            Assert.Empty(vm.Items);
        }

        [Fact]
        public void ItemsProperty_AfterOnViewAppearing_IsNotEmpty()
        {
            var vm = new ItemsViewModel(
                new Mock<INavigationService>().Object,
                mockDataStore.Object,
                new Mock<IMessagingCenter>().Object);
            vm.OnViewAppearing(this, new EventArgs());
            Assert.NotEmpty(vm.Items);
        }

        [Fact]
        public void ItemsProperty_LoadItemsCommandExecuted_RaisesCollectionChanged()
        {
            bool invoked = false;
            var vm = new ItemsViewModel(
                new Mock<INavigationService>().Object,
                mockDataStore.Object,
                new Mock<IMessagingCenter>().Object);
            var test = new ObservableRangeCollection<Item>();

            vm.Items.CollectionChanged += (sender, e) =>
            {
                if(e.Action == NotifyCollectionChangedAction.Reset
                    && (((ObservableRangeCollection<Item>)sender).Count == 1))
                        invoked = true;
            };

            vm.LoadItemsCommand.Execute(null);
            Assert.True(invoked);
        }

        [Fact]
        public void ItemsViewModel_Constructed_MessagingCenterSubscribeToAddItem()
        {
            var messagingCenter = new Mock<IMessagingCenter>();
            var callback = It.IsAny<Action<NewItemViewModel, Item>>();
            messagingCenter.Setup(o => o.Subscribe(
                    It.IsAny<ItemsViewModel>(),
                    "AddItem",
                    It.IsAny<Action<NewItemViewModel, Item>>(), null)
                ).Verifiable();

            var vm = new ItemsViewModel(
                new Mock<INavigationService>().Object,
                new Mock<IDataStore<Item>>().Object,
                messagingCenter.Object);

            messagingCenter.VerifyAll();
        }
    }
}
