Feature('FateAPI');

Scenario('Get Element by Birth Year', async ({ I }) => {
  const dob = '1990-01-01'; // Example date of birth
  const response = await I.sendGetRequest(`/api/Fate/element?dob=${dob}`);
  
  I.seeResponseCodeIs(200);
  I.seeResponseEquals('Thổ'); // Check if the response equals the expected text
});

Scenario('Get Mutualism by Element', async ({ I }) => {
    const element = 'Thủy'; // Example element
    const response = await I.sendGetRequest(`/api/Fate/Mutualism?e=${element}`);
    
    I.seeResponseCodeIs(200);
    I.seeResponseContainsJson({
        "elementId": "Thủy",
        "mutualism": "Kim",
        "quantityOfFish": null,
        "advertisements": [],
        "elementColors": [],
        "pointOfShapes": []
      });
  });

Scenario('Convert Solar Date to Lunar Date', async ({ I }) => {
  const YOB = '2004-10-13'; // Example Year of Birth
  const response = await I.sendGetRequest(`/api/Fate/ConvertToLunar?YOB=${YOB}`);
  
  I.seeResponseCodeIs(200);
  I.seeResponseContainsKeys(['lunarYear', 'lunarMonth', 'lunarDay']);
  I.seeResponseContainsJson({
    "lunarYear": 2004, 
    "lunarMonth": 8,  
    "lunarDay": 30      
    });
});

Scenario('Test Calculate Life_Palace', async ({ I }) => {
    const YOB = '2004-10-13'; // Example date of birth
    const gender = 'Male'; // Example gender
    const response = await I.sendGetRequest(`/api/Fate/CalculateLife_Palace?YOB=${YOB}&Gender=${gender}`);
    I.seeResponseCodeIs(200);
    I.seeResponseEquals('Khôn');
});
