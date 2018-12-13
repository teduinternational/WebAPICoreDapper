SELECT av.Id,
	   a.Name AS AttributeName,
	   av.Value AS AttributeValue
FROM AttributeValues av INNER JOIN Attributes a ON av.AttributeId = a.Id
WHERE av.Id IN
      (SELECT AttributeValueId
       FROM ProductAttributes
       WHERE ProductId = 3)