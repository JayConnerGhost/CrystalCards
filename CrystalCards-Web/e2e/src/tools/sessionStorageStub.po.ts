describe('Crystal Ideas Tool ', function() {
  it('should have a clean session  and local storage',  function() {
   try {
     browser.executeScript('window.sessionStorage.clear();');
     browser.executeScript('window.localStorage.clear();');
   }
   catch(e)
   {}
//hack...
  });
});
