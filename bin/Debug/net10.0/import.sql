-- =========================================================
-- 0. INSERINDO AS MESAS 
-- =========================================================
INSERT INTO Mesas (Numero, CapacidadePessoas) VALUES (1, 4);
INSERT INTO Mesas (Numero, CapacidadePessoas) VALUES (2, 4);
INSERT INTO Mesas (Numero, CapacidadePessoas) VALUES (3, 6);
INSERT INTO Mesas (Numero, CapacidadePessoas) VALUES (4, 4);
INSERT INTO Mesas (Numero, CapacidadePessoas) VALUES (5, 3);
INSERT INTO Mesas (Numero, CapacidadePessoas) VALUES (6, 2);
INSERT INTO Mesas (Numero, CapacidadePessoas) VALUES (7, 2);
INSERT INTO Mesas (Numero, CapacidadePessoas) VALUES (8, 2);
INSERT INTO Mesas (Numero, CapacidadePessoas) VALUES (9, 6);
INSERT INTO Mesas (Numero, CapacidadePessoas) VALUES (10, 4);

-- =========================================================
-- 1. INSERINDO INGREDIENTES (IDs de 1 a 20)
-- =========================================================
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Arroz Branco', 1);       -- 1
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Feijão Tropeiro', 1);    -- 2
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Carne de Sol', 1);       -- 3
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Peixe Fresco', 1);       -- 4
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Camarão Rosa', 1);       -- 5
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Peito de Frango', 1);    -- 6
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Massa Artesanal', 1);    -- 7
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Tomate Cereja', 1);      -- 8
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Queijo Coalho', 1);      -- 9
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Queijo Parmesão', 1);    -- 10
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Batata Rústica', 1);     -- 11
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Alface Americana', 1);   -- 12
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Azeite Trufado', 1);     -- 13
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Cebola Roxa', 1);        -- 14
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Filé Mignon', 1);        -- 15
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Linguiça Calabresa', 1); -- 16
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Bacon em Cubos', 1);     -- 17
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Farofa Crocante', 1);    -- 18
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Alho Dourado', 1);       -- 19
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Pimentão Amarelo', 1);   -- 20

-- =========================================================
-- 2. INSERINDO PRATOS DE ALMOÇO (Período = 0) (IDs de 1 a 20)
-- =========================================================
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Rubacão Tocantinense', 'Clássico prato com feijão, arroz, carne de sol e muito queijo.', 45.00, 0, 1, '/img/baiao.png'); -- 1
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Bife a Cavalo', 'Suculenta carne acompanhada de arroz, fritas e ovo frito.', 38.00, 0, 1, '/img/baiao.png'); -- 2
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Frango Fit Grelhado', 'Peito de frango grelhado na manteiga com salada fresca.', 32.00, 0, 1, '/img/baiao.png'); -- 3
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Peixe Frito com Pirão', 'Peixe da região frito na hora, acompanha pirão e arroz.', 42.00, 0, 1, '/img/baiao.png'); -- 4
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Escondidinho de Carne', 'Purê cremoso de batata recheado com carne de sol.', 39.00, 0, 1, '/img/baiao.png'); -- 5
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Baião de Dois', 'Mistura perfeita de arroz, feijão e pedaços de queijo coalho.', 36.00, 0, 1, '/img/baiao.png'); -- 6
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Estrogonofe de Frango', 'Estrogonofe cremoso acompanhado de arroz e batata.', 34.00, 0, 1, '/img/baiao.png'); -- 7
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Macarronada Rústica', 'Massa fresca ao molho de tomate com pedaços de carne.', 37.00, 0, 1, '/img/baiao.png'); -- 8
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Salada Completa', 'Mix de folhas verdes, tomate, cebola e frango desfiado.', 28.00, 0, 1, '/img/baiao.png'); -- 9
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Prato do Trabalhador', 'Arroz, feijão tropeiro e dois suculentos filés de frango.', 25.00, 0, 1, '/img/baiao.png'); -- 10
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Feijoada Completa', 'Feijoada tradicional com arroz, farofa e couve.', 48.00, 0, 1, '/img/baiao.png'); -- 11
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Picadinho de Mignon', 'Picadinho de carne com arroz e farofa.', 42.00, 0, 1, '/img/baiao.png'); -- 12
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Bife Acebolado', 'Bife grelhado com muita cebola e arroz branco.', 35.00, 0, 1, '/img/baiao.png'); -- 13
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Iscas de Peixe', 'Tirinhas de peixe empanadas com batata rústica.', 38.00, 0, 1, '/img/baiao.png'); -- 14
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Panqueca de Frango', 'Massa artesanal recheada com frango desfiado.', 30.00, 0, 1, '/img/baiao.png'); -- 15
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Omelete de Queijo', 'Omelete de queijo coalho acompanhado de salada.', 22.00, 0, 1, '/img/baiao.png'); -- 16
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Salpicão de Frango', 'Salpicão refrescante com frango e legumes.', 26.00, 0, 1, '/img/baiao.png'); -- 17
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Carne de Panela', 'Carne cozida lentamente com batatas e arroz.', 39.00, 0, 1, '/img/baiao.png'); -- 18
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Nhoque ao Sugo', 'Massa artesanal de nhoque ao molho de tomates frescos.', 34.00, 0, 1, '/img/baiao.png'); -- 19
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Frango à Passarinho', 'Cortes de frango fritos com alho dourado e arroz.', 31.00, 0, 1, '/img/baiao.png'); -- 20

-- =========================================================
-- 3. INSERINDO PRATOS DE JANTAR (Período = 1) (IDs de 21 a 40)
-- =========================================================
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Risoto de Camarão', 'Arroz arbóreo cremoso com camarões grandes e parmesão.', 68.00, 1, 1, '/img/baiao.png'); -- 21
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Filé Mignon ao Molho', 'Medalhões de mignon banhados em molho de vinho e batatas.', 85.00, 1, 1, '/img/baiao.png'); -- 22
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Salmão com Ervas', 'Posta de salmão grelhada com azeite trufado e salada.', 75.00, 1, 1, '/img/baiao.png'); -- 23
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Camarão na Moranga', 'Camarões puxados no creme de queijo dentro da moranga.', 92.00, 1, 1, '/img/baiao.png'); -- 24
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Espaguete ao Pesto', 'Massa artesanal ao molho pesto com tomates confitados.', 54.00, 1, 1, '/img/baiao.png'); -- 25
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Moqueca Baiana', 'Moqueca de peixe e camarão no azeite de dendê.', 88.00, 1, 1, '/img/baiao.png'); -- 26
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Frango à Parmegiana', 'Peito de frango empanado, coberto com queijo e molho.', 48.00, 1, 1, '/img/baiao.png'); -- 27
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Sopa de Cebola', 'Clássica sopa francesa gratinada com queijo parmesão.', 42.00, 1, 1, '/img/baiao.png'); -- 28
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Tábua de Frios', 'Seleção de queijos e tomates para compartilhar.', 60.00, 1, 1, '/img/baiao.png'); -- 29
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Lasanha aos 4 Queijos', 'Massa artesanal intercalada com muito queijo derretido.', 58.00, 1, 1, '/img/baiao.png'); -- 30
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Pizza Margherita', 'Massa artesanal, tomate, queijo e manjericão.', 55.00, 1, 1, '/img/baiao.png'); -- 31
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Hambúrguer Gourmet', 'Blend de filé mignon, bacon, queijo e batatas.', 45.00, 1, 1, '/img/baiao.png'); -- 32
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Caldo de Feijão', 'Caldo quente de feijão com bacon crocante e pão.', 28.00, 1, 1, '/img/baiao.png'); -- 33
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Ceviche de Peixe', 'Peixe fresco marinado com cebola roxa e limão.', 48.00, 1, 1, '/img/baiao.png'); -- 34
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Yakisoba Especial', 'Massa com frango, carne, legumes e molho de soja.', 52.00, 1, 1, '/img/baiao.png'); -- 35
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Fondue de Queijo', 'Mistura de queijos finos para mergulhar pães e carnes.', 110.00, 1, 1, '/img/baiao.png'); -- 36
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Tacos Mexicanos', 'Tortilhas recheadas com carne, alface e queijo.', 44.00, 1, 1, '/img/baiao.png'); -- 37
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Camarão Alho e Óleo', 'Porção de camarões salteados no alho dourado.', 65.00, 1, 1, '/img/baiao.png'); -- 38
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Risoto de Funghi', 'Arroz arbóreo com cogumelos e queijo parmesão.', 62.00, 1, 1, '/img/baiao.png'); -- 39
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES ('Salada Caprese', 'Fatias de tomate fresco, queijo coalho e azeite.', 35.00, 1, 1, '/img/baiao.png'); -- 40

-- =========================================================
-- 4. VINCULANDO INGREDIENTES AOS PRATOS
-- =========================================================

-- ALMOÇO
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (1, 1), (2, 1), (3, 1), (9, 1); -- 1. Rubacão
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (1, 2), (11, 2), (15, 2); -- 2. Bife a Cavalo
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (6, 3), (12, 3), (8, 3); -- 3. Frango Fit
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (4, 4), (1, 4), (18, 4); -- 4. Peixe Frito
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (11, 5), (3, 5), (9, 5); -- 5. Escondidinho (Batata, Carne Sol, Queijo)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (1, 6), (2, 6), (3, 6), (9, 6); -- 6. Baião (Arroz, Feijão, Carne Sol, Queijo)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (6, 7), (1, 7), (11, 7); -- 7. Estrogonofe (Frango, Arroz, Batata)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (7, 8), (8, 8), (15, 8); -- 8. Macarronada (Massa, Tomate, Carne)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (12, 9), (8, 9), (14, 9), (6, 9); -- 9. Salada (Alface, Tomate, Cebola, Frango)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (1, 10), (2, 10), (6, 10); -- 10. Prato Trabalhador (Arroz, Feijão, Frango)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (1, 11), (18, 11), (16, 11), (17, 11); -- 11. Feijoada (Arroz, Farofa, Calabresa, Bacon)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (15, 12), (1, 12), (18, 12); -- 12. Picadinho (Mignon, Arroz, Farofa)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (15, 13), (14, 13), (1, 13); -- 13. Bife Acebolado (Mignon, Cebola, Arroz)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (4, 14), (11, 14); -- 14. Iscas de Peixe (Peixe, Batata)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (7, 15), (6, 15); -- 15. Panqueca (Massa, Frango)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (9, 16), (12, 16), (8, 16); -- 16. Omelete Queijo (Queijo, Alface, Tomate)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (6, 17), (20, 17), (14, 17); -- 17. Salpicão (Frango, Pimentão, Cebola)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (15, 18), (11, 18), (1, 18); -- 18. Carne Panela (Carne, Batata, Arroz)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (7, 19), (8, 19); -- 19. Nhoque (Massa, Tomate)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (6, 20), (19, 20), (1, 20); -- 20. Frango Passarinho (Frango, Alho, Arroz)

-- JANTAR
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (1, 21), (5, 21), (10, 21); -- 21. Risoto Camarão
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (11, 22), (15, 22); -- 22. Filé Mignon
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (4, 23), (13, 23), (12, 23); -- 23. Salmão (Peixe, Azeite, Alface)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (5, 24), (9, 24), (10, 24); -- 24. Camarão Moranga (Camarão, Queijos)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (7, 25), (8, 25), (10, 25), (13, 25); -- 25. Espaguete
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (4, 26), (5, 26), (14, 26), (8, 26); -- 26. Moqueca
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (6, 27), (10, 27), (8, 27); -- 27. Frango Parmegiana (Frango, Queijo, Tomate)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (14, 28), (10, 28); -- 28. Sopa Cebola (Cebola, Queijo)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (9, 29), (10, 29), (8, 29); -- 29. Tábua Frios (Queijos, Tomate)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (7, 30), (9, 30), (10, 30); -- 30. Lasanha Queijos (Massa, Queijos)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (7, 31), (8, 31), (9, 31), (10, 31); -- 31. Pizza Margherita (Massa, Tomate, Queijos)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (15, 32), (17, 32), (9, 32), (11, 32); -- 32. Hambúrguer (Carne, Bacon, Queijo, Batata)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (2, 33), (17, 33), (19, 33); -- 33. Caldo Feijão (Feijão, Bacon, Alho)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (4, 34), (14, 34), (20, 34); -- 34. Ceviche (Peixe, Cebola, Pimentão)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (7, 35), (6, 35), (15, 35), (20, 35); -- 35. Yakisoba (Massa, Frango, Carne, Pimentão)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (9, 36), (10, 36), (15, 36); -- 36. Fondue (Queijos, Carne)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (7, 37), (15, 37), (12, 37), (9, 37); -- 37. Tacos (Massa, Carne, Alface, Queijo)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (5, 38), (19, 38); -- 38. Camarão Alho/Óleo (Camarão, Alho)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (1, 39), (10, 39), (13, 39); -- 39. Risoto Funghi (Arroz, Queijo, Azeite Trufado)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (8, 40), (9, 40), (13, 40); -- 40. Salada Caprese (Tomate, Queijo, Azeite)