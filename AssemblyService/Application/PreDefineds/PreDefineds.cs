using Application.Generics.Dtos;
using System.Text;

namespace Application.PreDefineds
{
    internal static class PreDefineds
    {
        public static int ProductType = 180450;

        //Orga Module
        public static string OrgaItems = $"/orga/orgaItems?productType={ProductType}";

        public static Func<(int agencyId, int insurerId, int underwriterId, string recommenderUniqueId), string> Recommenders = (param) => $"/orga/recommenders?productType={ProductType}&agencyId={param.agencyId}&insurerId={param.insurerId}&underwriterId={param.underwriterId}&recommenderUniqueId={param.recommenderUniqueId}";
        public static Func<int, string> Protocols = (underwriterId) => $"/orga/protocols?productType={ProductType}&underwriterId={underwriterId}";
    }
}
