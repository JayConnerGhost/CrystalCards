import {TargetedEntryPointFinder} from '@angular/compiler-cli/ngcc/src/entry_point_finder/targeted_entry_point_finder';

describe( 'Crystal Ideas, business Process', () => {
  it("should be able to delete a card", () => {
    const baseUrl = 'http://localhost:4200/';
    // Arrange
    await browser.driver.manage().window().maximize() ;
    await browser.get(baseUrl);

    // clean down
    await element.all(by.css('.card-token')).each(function(el,index) {
      el.element(by.id('deleteButton')).click();
    });
    await element(by.id('loginButton')).click();
    await browser.waitForAngular();
    await element(by.id('usernameField')).sendKeys('Test');
    await element(by.id('passwordField')).sendKeys('test');
    await element(by.id('loginButton')).click();
    await browser.waitForAngular();
    // create a target card
    await  element(by.id('addCardButton')).click();
    await browser.waitForAngular();
    await browser.waitForAngular();
    await  element(by.id("titleField")).sendKeys('test title');
    await element(by.id('descriptionField')).sendKeys('test description');
    await element(by.id('submitButton')).click();
    await browser.waitForAngular();
    // Act
    target=await element.all(by.css('.card-token')).first();
    target.element(by.id('deleteButton')).click();

    // assert
    expect(numberOfCards).toEqual(0);

  });

});
