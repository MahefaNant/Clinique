

DROP VIEW IF EXISTS v_budgetaire;
DROP VIEW IF EXISTS v_somme_depense_paransmois_withbudget;
DROP VIEW IF EXISTS v_somme_depense_paransmoistypeacte_withbudget;
DROP VIEW IF EXISTS v_somme_depense_paransmois;
DROP VIEW IF EXISTS v_somme_acte_paransmois_withbudget;
DROP VIEW IF EXISTS v_somme_acte_paransmoistypeacte_withbudget;
DROP VIEW IF EXISTS v_somme_acte_paransmois;
DROP VIEW IF EXISTS v_somme_facture;
DROP VIEW IF EXISTS v_facture_acte;



CREATE OR REPLACE VIEW v_facture_acte AS
    SELECT
        acte.id_typeacte,acte.id_facture,acte.montant,
        facture_patient.id_patient,
        facture_patient.date AS facture_date
    FROM
        acte
            INNER JOIN
        facture_patient
        ON
                acte.id_facture = facture_patient.id;

CREATE OR REPLACE VIEW v_somme_facture AS
SELECT
    id_facture,
    SUM(montant) AS total_montant
FROM
    v_facture_acte
GROUP BY
    id_facture;

----------------------------------------------------------------
-----------------------ACTE--------------------------------------
----------------------------------------------------------------

CREATE OR REPLACE VIEW v_somme_acte_paransmois AS
    SELECT
        id_typeacte,
        EXTRACT(YEAR FROM facture_date) AS annee,
        EXTRACT(MONTH FROM facture_date) AS mois,
        ROUND (SUM(montant),2) AS total_montant
    FROM
        v_facture_acte
    GROUP BY
        EXTRACT(YEAR FROM facture_date),
        EXTRACT(MONTH FROM facture_date),
        id_typeacte
    ORDER BY
        annee,
        mois,
        id_typeacte;

CREATE OR REPLACE VIEW v_typeacte_all AS
    SELECT
        budget.id_typeacte,
        budget.annee,
        budget.budget,
        typeacte.nom,
        typeacte.code
    FROM
        typeacte
            JOIN
        budgetacte AS budget
        ON
                typeacte.id = budget.id_typeacte;


CREATE OR REPLACE VIEW v_somme_acte_paransmoistypeacte_withbudget AS
    SELECT v.id_typeacte, v.annee, v.mois,  v.total_montant, ROUND(b.budget/12,2) as budget,
           ROUND( (v.total_montant*100) / (b.budget/12) ,2) as realisation
    FROM v_somme_acte_paransmois v
             LEFT JOIN budgetacte b ON v.id_typeacte = b.id_typeacte AND v.annee = b.annee;

SELECT
    v_typeacte_all.id,
    v_typeacte_all.id_typeacte,
    v_typeacte_all.annee,
    ROUND( v_typeacte_all.budget/12) as budget,
    v_typeacte_all.nom,
    v_typeacte_all.code,
    COALESCE(v_somme_acte_paransmoistypeacte_withbudget.mois, 0) AS mois,
    COALESCE(v_somme_acte_paransmoistypeacte_withbudget.total_montant, 0) AS total_montant,
    COALESCE(v_somme_acte_paransmoistypeacte_withbudget.realisation, 0) AS realisation
FROM
    v_typeacte_all
        LEFT JOIN
    v_somme_acte_paransmoistypeacte_withbudget
    ON
            v_typeacte_all.id_typeacte = v_somme_acte_paransmoistypeacte_withbudget.id_typeacte;



CREATE OR REPLACE VIEW v_somme_acte_paransmois_withbudget AS
    SELECT
        annee,
        mois,
        ROUND(SUM(total_montant),2) AS sum_total_montant,
        ROUND(SUM(budget),2) AS sum_budget,
        ROUND(SUM(realisation),2) as realisation
    FROM v_somme_acte_paransmoistypeacte_withbudget
    GROUP BY annee, mois;

------------------------------------------------------------------------------
------------------------------------------------------------------------------

----------------------------------------------------------------
-----------------------DEPENSE--------------------------------------
----------------------------------------------------------------


CREATE OR REPLACE VIEW v_somme_depense_paransmois AS
SELECT
    EXTRACT(YEAR FROM date) AS annee,
    EXTRACT(MONTH FROM date) AS mois,
    ROUND(SUM(montant),2) AS montant_total,
    id_typedepense
FROM
    depense
GROUP BY
    EXTRACT(YEAR FROM date),
    EXTRACT(MONTH FROM date),
    id_typedepense
ORDER BY
    EXTRACT(YEAR FROM date),
    EXTRACT(MONTH FROM date),
    id_typedepense;

CREATE OR REPLACE VIEW v_typedepense_all AS
SELECT
    budget.id_typedepense,
    budget.annee,
    budget.budget,
    typedepense.nom,
    typedepense.code
FROM
    typedepense
        JOIN
    budgedepense AS budget
    ON
            typedepense.id = budget.id_typedepense;

CREATE OR REPLACE VIEW v_somme_depense_paransmoistypeacte_withbudget AS
SELECT v.id_typedepense, v.annee, v.mois, v.montant_total,ROUND(b.budget/12,2) as budget,
       ROUND( (v.montant_total*100) / (b.budget/12) ,2) as realisation
FROM v_somme_depense_paransmois v
         LEFT JOIN budgedepense b ON v.id_typedepense = b.id_typedepense AND v.annee = b.annee;


CREATE OR REPLACE VIEW v_somme_depense_paransmois_withbudget AS
SELECT
    annee,
    mois,
    ROUND(SUM(montant_total),2) AS sum_montant_total,
    ROUND(SUM(budget),2) AS sum_budget,
    ROUND(SUM(realisation),2) as realisation
FROM v_somme_depense_paransmoistypeacte_withbudget
GROUP BY annee, mois;

-----------------------------------------------------------------
-----------------------------------------------------------------

CREATE OR REPLACE VIEW v_budgetaire AS 
SELECT 
       COALESCE(acte.annee, depense.annee) AS annee,
       COALESCE(acte.mois, depense.mois) AS mois,
       ROUND( (COALESCE(acte.sum_total_montant ,0) - COALESCE(depense.sum_montant_total,0)) ,2 ) AS sum_reel,
       ROUND( (COALESCE(acte.sum_budget ,0) - COALESCE(depense.sum_budget,0)) , 2) AS sum_budget,
       ROUND( (COALESCE(acte.realisation ,0) - COALESCE(depense.realisation,0)),2) AS sum_realisation
--        CASE WHEN (COALESCE(acte.realisation ,0) - COALESCE(depense.realisation,0)) < 0 THEN 0
--             ELSE ROUND( (COALESCE(acte.realisation ,0) - COALESCE(depense.realisation,0)),2)
--            END AS sum_realisation
    FROM
        v_somme_acte_paransmois_withbudget AS acte
            FULL OUTER JOIN
        v_somme_depense_paransmois_withbudget AS depense
        ON
                    acte.annee = depense.annee
                AND acte.mois = depense.mois
    ORDER BY
        annee,
        mois;

-- CREATE OR REPLACE VIEW v_budgetaire AS
--     SELECT
--         COALESCE(acte.total_montant, 0)  AS acte_montant,
--         COALESCE(depense.montant_total,0) AS depense_montant,
--         COALESCE(acte.annee, depense.annee) AS annee,
--         COALESCE(acte.mois, depense.mois) AS mois,
--         ROUND(COALESCE(acte.total_montant, 0) - COALESCE(depense.montant_total, 0),2) AS difference_montant
--     FROM
--         v_somme_acte_paransmois AS acte
--             FULL OUTER JOIN
--         v_somme_depense_paransmois AS depense
--         ON
--                     acte.annee = depense.annee
--                 AND acte.mois = depense.mois
--     ORDER BY
--         annee,
--         mois;





