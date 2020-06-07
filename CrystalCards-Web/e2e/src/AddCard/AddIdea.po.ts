﻿

describe('Crystal Ideas , business process', function() {
  it('should be able to add a card', async function() {
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

    // Act
    await  element(by.id('addCardButton')).click();
    await browser.waitForAngular();
    await browser.waitForAngular();
    await  element(by.id("titleField")).sendKeys('test title');
    await element(by.id('descriptionField')).sendKeys('test description');
    await element(by.id('submitButton')).click();
    await browser.waitForAngular();

    // Assert
    await element(by.id('showAsDeckButton')).click();
    const numberOfCards = await element.all(by.css('.card-token')).count();
    expect(numberOfCards).toEqual(1);

    // Clean up
    await element.all(by.css('.card-token')).each(function(el,index) {
      el.element(by.id('deleteButton')).click();
  });
  });
});
