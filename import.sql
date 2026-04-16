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
-- Ingredientes Novos
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Linguiça Calabresa', 1); -- 16
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Bacon em Cubos', 1);     -- 17
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Farofa Crocante', 1);    -- 18
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Alho Dourado', 1);       -- 19
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Pimentão Amarelo', 1);   -- 20

-- =========================================================
-- 2. INSERINDO PRATOS DE ALMOÇO (Período = 0) (IDs de 1 a 20)
-- Total exigido: 20 pratos
-- =========================================================
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Rubacão Tocantinense', 'Clássico prato com feijão, arroz, carne de sol e muito queijo.', 45.00, 0, 1); -- 1
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Bife a Cavalo', 'Suculenta carne acompanhada de arroz, fritas e ovo frito.', 38.00, 0, 1); -- 2
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Frango Fit Grelhado', 'Peito de frango grelhado na manteiga com salada fresca.', 32.00, 0, 1); -- 3
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Peixe Frito com Pirão', 'Peixe da região frito na hora, acompanha pirão e arroz.', 42.00, 0, 1); -- 4
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Escondidinho de Carne', 'Purê cremoso de batata recheado com carne de sol.', 39.00, 0, 1); -- 5
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Baião de Dois', 'Mistura perfeita de arroz, feijão e pedaços de queijo coalho.', 36.00, 0, 1); -- 6
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Estrogonofe de Frango', 'Estrogonofe cremoso acompanhado de arroz e batata.', 34.00, 0, 1); -- 7
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Macarronada Rústica', 'Massa fresca ao molho de tomate com pedaços de carne.', 37.00, 0, 1); -- 8
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Salada Completa', 'Mix de folhas verdes, tomate, cebola e frango desfiado.', 28.00, 0, 1); -- 9
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Prato do Trabalhador', 'Arroz, feijão tropeiro e dois suculentos filés de frango.', 25.00, 0, 1); -- 10
-- Novos Almoços
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Feijoada Completa', 'Feijoada tradicional com arroz, farofa e couve.', 48.00, 0, 1); -- 11
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Picadinho de Mignon', 'Picadinho de carne com arroz e farofa.', 42.00, 0, 1); -- 12
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Bife Acebolado', 'Bife grelhado com muita cebola e arroz branco.', 35.00, 0, 1); -- 13
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Iscas de Peixe', 'Tirinhas de peixe empanadas com batata rústica.', 38.00, 0, 1); -- 14
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Panqueca de Frango', 'Massa artesanal recheada com frango desfiado.', 30.00, 0, 1); -- 15
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Omelete de Queijo', 'Omelete de queijo coalho acompanhado de salada.', 22.00, 0, 1); -- 16
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Salpicão de Frango', 'Salpicão refrescante com frango e legumes.', 26.00, 0, 1); -- 17
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Carne de Panela', 'Carne cozida lentamente com batatas e arroz.', 39.00, 0, 1); -- 18
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Nhoque ao Sugo', 'Massa artesanal de nhoque ao molho de tomates frescos.', 34.00, 0, 1); -- 19
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Frango à Passarinho', 'Cortes de frango fritos com alho dourado e arroz.', 31.00, 0, 1); -- 20

-- =========================================================
-- 3. INSERINDO PRATOS DE JANTAR (Período = 1) (IDs de 21 a 40)
-- Total exigido: 20 pratos
-- =========================================================
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Risoto de Camarão', 'Arroz arbóreo cremoso com camarões grandes e parmesão.', 68.00, 1, 1); -- 21
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Filé Mignon ao Molho', 'Medalhões de mignon banhados em molho de vinho e batatas.', 85.00, 1, 1); -- 22
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Salmão com Ervas', 'Posta de salmão grelhada com azeite trufado e salada.', 75.00, 1, 1); -- 23
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Camarão na Moranga', 'Camarões puxados no creme de queijo dentro da moranga.', 92.00, 1, 1); -- 24
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Espaguete ao Pesto', 'Massa artesanal ao molho pesto com tomates confitados.', 54.00, 1, 1); -- 25
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Moqueca Baiana', 'Moqueca de peixe e camarão no azeite de dendê.', 88.00, 1, 1); -- 26
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Frango à Parmegiana', 'Peito de frango empanado, coberto com queijo e molho.', 48.00, 1, 1); -- 27
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Sopa de Cebola', 'Clássica sopa francesa gratinada com queijo parmesão.', 42.00, 1, 1); -- 28
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Tábua de Frios', 'Seleção de queijos e tomates para compartilhar.', 60.00, 1, 1); -- 29
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Lasanha aos 4 Queijos', 'Massa artesanal intercalada com muito queijo derretido.', 58.00, 1, 1); -- 30
-- Novos Jantares
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Pizza Margherita', 'Massa artesanal, tomate, queijo e manjericão.', 55.00, 1, 1); -- 31
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Hambúrguer Gourmet', 'Blend de filé mignon, bacon, queijo e batatas.', 45.00, 1, 1); -- 32
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Caldo de Feijão', 'Caldo quente de feijão com bacon crocante e pão.', 28.00, 1, 1); -- 33
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Ceviche de Peixe', 'Peixe fresco marinado com cebola roxa e limão.', 48.00, 1, 1); -- 34
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Yakisoba Especial', 'Massa com frango, carne, legumes e molho de soja.', 52.00, 1, 1); -- 35
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Fondue de Queijo', 'Mistura de queijos finos para mergulhar pães e carnes.', 110.00, 1, 1); -- 36
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Tacos Mexicanos', 'Tortilhas recheadas com carne, alface e queijo.', 44.00, 1, 1); -- 37
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Camarão Alho e Óleo', 'Porção de camarões salteados no alho dourado.', 65.00, 1, 1); -- 38
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Risoto de Funghi', 'Arroz arbóreo com cogumelos e queijo parmesão.', 62.00, 1, 1); -- 39
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) VALUES ('Salada Caprese', 'Fatias de tomate fresco, queijo coalho e azeite.', 35.00, 1, 1); -- 40

-- =========================================================
-- 4. VINCULANDO INGREDIENTES AOS PRATOS
-- =========================================================
-- Alguns exemplos principais (Você pode adicionar os outros depois se desejar)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (1, 1), (2, 1), (3, 1), (9, 1); -- Rubacão
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (1, 2), (11, 2), (15, 2); -- Bife a Cavalo
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (6, 3), (12, 3), (8, 3); -- Frango Fit
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (4, 4), (1, 4), (18, 4); -- Peixe Frito (com farofa)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (1, 21), (5, 21), (10, 21); -- Risoto Camarão
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (11, 22), (15, 22); -- Filé Mignon
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (7, 25), (8, 25), (10, 25), (13, 25); -- Espaguete
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (4, 26), (5, 26), (14, 26), (8, 26); -- Moqueca