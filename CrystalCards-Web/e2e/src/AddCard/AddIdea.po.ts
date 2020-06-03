﻿

describe('Crystal Ideas , business process', function() {
  it('should be able to add a card', function() {
    const baseUrl = 'http://localhost:4200/';
    // Arrange
    browser.driver.manage().window().maximize() ;
    browser.get(baseUrl);

    element(by.id('loginButton')).click();

    browser.waitForAngular();

    element(by.id('usernameField')).sendKeys('Test');
    element(by.id('passwordField')).sendKeys('test');
    element(by.id('loginButton')).click();

    browser.waitForAngular();
    // Act
    element(by.id('addCardButton')).click();
    browser.waitForAngular();
    // Assert
    browser.sleep(1000);

    //element(by.id('logoutButton')).click();

    browser.waitForAngular();

  });
});
