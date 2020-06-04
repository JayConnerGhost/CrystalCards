﻿

describe('Crystal Ideas , business process', function() {
  it('should be able to add a card', async function() {
    const baseUrl = 'http://localhost:4200/';
    // Arrange
  await browser.driver.manage().window().maximize() ;
    await browser.get(baseUrl);

    await element(by.id('loginButton')).click();

    await browser.waitForAngular();

    await element(by.id('usernameField')).sendKeys('Test');
    await element(by.id('passwordField')).sendKeys('test');
    await element(by.id('loginButton')).click();

    await browser.waitForAngular();
    // Act
    await  element(by.id('addCardButton')).click();
    await browser.waitForAngular();
    // Assert
    await browser.sleep(1000);

    //element(by.id('logoutButton')).click();

    await browser.waitForAngular();
    await browser.sleep(1000);

    await  element(by.id("titleField")).sendKeys('test title');
    await element(by.id('descriptionField')).sendKeys('test description');
    await element(by.id('submitButton')).click();
    await browser.waitForAngular();
    await browser.sleep(1000);

    await element(by.id('showAsDeckButton')).click();

    await element.all(by.css('.card-token')).each(function(el,index) {
      el.element(by.id('deleteButton')).click();
  });
      });
});
