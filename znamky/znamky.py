soubor = open("data.txt", encoding = "utf-8")
soubor.readline()

prijmeni = []
jmeno = []
znamka = []


for radek in soubor:
    radek = radek.replace('\n', '')
    polozky = (radek.split(","))
    
    
    
    
    prijmeni.append(polozky [0])
    jmeno.append(polozky [1])
    znamka.append(polozky [2])
    



    pocet_zaku = len(prijmeni)

    
    


soubor.close()



def prumer(seznam):

    soucet = 0
    pocet_cisel = 0

    for prvek in seznam:
            soucet += float(prvek)  # Převod na číselnou hodnotu a přidání k součtu
            pocet_cisel += 1  

    prumer = soucet / pocet_cisel
    return prumer

vysledek = prumer(znamka)


hledana_hodnota = min(znamka)
index = znamka.index(hledana_hodnota)

hledana_hodnota_1 = max(znamka)
index_1 = znamka.index(hledana_hodnota_1)
   
    
print("Nejlepší známku má:",prijmeni[index])
print("Nejhorší známku má:",prijmeni[index_1])


print("Počet žáků ve třídě je:", pocet_zaku)
print("Průměr čísel v seznamu je:", vysledek)
