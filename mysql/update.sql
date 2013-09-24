update articulo set categoria=null 
where categoria not in (select id from categoria)



