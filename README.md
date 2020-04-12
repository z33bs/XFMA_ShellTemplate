# XFMA_ShellTemplate
A refactored version of the Xamarin Forms Startup project, "Shell Forms App". The project has been refactored to implement  **ViewModel-First** navigation. It does this using  [XamarinFormsMvvmAdaptor](https://github.com/z33bs/xamarin-forms-mvvm-adaptor). This project also gives an example of Unit testing ViewModels using xUnit and Moq.

*Tip: Search for `//XFMA` in the solution to quickly jump to code comments explaining XamarinFormsMvvmAdaptor*



You will notice:

* Nothing in the code-behind files
* Simple. Almost nothing new to learn. Design pages as you normally would using Xamarin Forms.
* ViewModels are easily testable. This example uses xUnit and Moq to test the ItemsViewModel class.



When browsing the code, look for:

* DI container setup in App.cs. Note the user-friendliness, requiring minimal code. For enterprise development, enabling strict mode enforces discipline.
* Views wire-up to their corresponding ViewModels simply by adding:
```c#
mvvm:ViewModelLocator.AutoWireViewModel="True"
```
* Navigation from ItemsViewModel that passes an Item object to the NewItemViewModel:
```c#
await navigationService.PushAsync<ItemDetailViewModel>(item)
```
* Navigation using routes from DogsViewModel:
```c#
await navigationService.GoToAsync($"dogDetail?dogId={item.Id}")
```
* Use of XFMA's `OnViewAppearing` method in ItemsViewModel and DogsViewModel. This handles the Page.Appearing event, effectively a mirror of Page.OnAppearing.
