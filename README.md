# XFMA_ShellTemplate
A refactored version of the Xamarin Forms Startup project, "Shell Forms App". The project has been refactored to implement  **ViewModel-First** navigation. It does this using  [XamarinFormsMvvmAdaptor](https://github.com/z33bs/xamarin-forms-mvvm-adaptor). This project also gives an example of Unit testing ViewModels using NUnit and Moq.

*Tip: Search for `//XFMA` in the solution to quickly jump to code comments explaining XamarinFormsMvvmAdaptor*



You will notice:

* Nothing in the code-behind files
* Simple. Almost nothing new to learn. Design pages as you normally would using Xamarin Forms.
* ViewModels are easily testable. This example uses NUnit and Moq.



When browsing the code, look for:

* Setting up the DI container in App.cs. Note the user-friendliness, requiring minimal code. For enterprise development, enabling strict mode enforces discipline.
* All Views hookup to their corresponding ViewModels simply by adding:
```c#
mvvm:ViewModelLocator.AutoWireViewModel="True"
```
* Navigating from ItemsViewModel while passing data to the NewItemViewModel with:
```c#
await navigationService.PushAsync<ItemDetailViewModel>(item)
```
* Navigating from DogsViewModel using routes with:
```c#
await navigationService.GoToAsync($"dogDetail?dogId={item.Id}")
```
* Use of the `OnViewAppearing` method in ItemsViewModel and DogsViewModel. This mirrors the functionality of OnAppearing in the Page class.

